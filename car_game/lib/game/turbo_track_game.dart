import 'package:car_game/game/bomb.dart';
import 'package:flame/game.dart';
import 'package:flame/input.dart';
import 'package:flutter/material.dart';
import 'player_car.dart';
import 'background.dart';
import 'package:flutter/services.dart';
import 'package:flame/events.dart';
import 'dart:math';
import 'package:flame/components.dart';

class TurboTrackGame extends FlameGame with HasKeyboardHandlerComponents {
  static const double roadWidth = 240;
  static const double laneWidth = 80;
  late double roadLeft;

  late PlayerCar playerCar;
  bool moveLeftPressed = false;
  bool moveRightPressed = false;

  final List<Bomb> bombs = [];
  final Random random = Random();
  double bombSpawnTimer = 0;
  double bombSpawnInterval = 1.2; // seconds
  bool isGameOver = false;
  int score = 0;
  TextComponent? scoreText;

  @override
  Future<void> onLoad() async {
    super.onLoad();
    add(
      Background()
        ..size = size
        ..position = Vector2.zero(),
    );
    roadLeft = (size.x - roadWidth) / 2;
    playerCar = PlayerCar(roadLeft: roadLeft, laneWidth: laneWidth);
    await add(playerCar);
    scoreText = TextComponent(
      text: 'Score: 0',
      position: Vector2(size.x / 2, 60),
      anchor: Anchor.topCenter,
      textRenderer: TextPaint(
        style: const TextStyle(
          color: Colors.white,
          fontSize: 28,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
    add(scoreText!);
  }

  void spawnBomb() {
    int lane = random.nextInt(3);
    Bomb bomb = Bomb(
      lane: lane,
      y: -Bomb.bombSize,
      roadLeft: roadLeft,
      laneWidth: laneWidth,
    );
    bombs.add(bomb);
    add(bomb);
  }

  @override
  void update(double dt) {
    super.update(dt);
    if (!isGameOver) {
      if (moveLeftPressed) {
        playerCar.moveLeft(dt);
      }
      if (moveRightPressed) {
        playerCar.moveRight(dt);
      }
      // Bomb spawning
      bombSpawnTimer += dt;
      if (bombSpawnTimer >= bombSpawnInterval) {
        bombSpawnTimer = 0;
        spawnBomb();
      }
      // Remove bombs off screen and increase score
      bombs.removeWhere((bomb) {
        if (bomb.position.y > size.y) {
          score++;
          bomb.removeFromParent();
          return true;
        }
        return false;
      });
      // Update score text
      if (scoreText != null) {
        scoreText!.text = 'Score: $score';
      }
      // Collision detection
      for (final bomb in bombs) {
        if (playerCar.toRect().overlaps(bomb.toRect())) {
          isGameOver = true;
          break;
        }
      }
    }
  }

  void resetGame() {
    isGameOver = false;
    score = 0;
    for (final bomb in bombs) {
      bomb.removeFromParent();
    }
    bombs.clear();
    playerCar.position = Vector2(
      roadLeft + (laneWidth * 1.5) - (PlayerCar.carWidth / 2),
      size.y - PlayerCar.carHeight - 20,
    );
    if (scoreText != null) {
      scoreText!.text = 'Score: 0';
      scoreText!.position = Vector2(size.x / 2, 20);
      scoreText!.anchor = Anchor.topCenter;
    }
  }

  @override
  KeyEventResult onKeyEvent(
    KeyEvent event,
    Set<LogicalKeyboardKey> keysPressed,
  ) {
    if (isGameOver) {
      moveLeftPressed = false;
      moveRightPressed = false;
      return KeyEventResult.handled;
    }
    moveLeftPressed = keysPressed.contains(LogicalKeyboardKey.arrowLeft);
    moveRightPressed = keysPressed.contains(LogicalKeyboardKey.arrowRight);
    return KeyEventResult.handled;
  }
}
