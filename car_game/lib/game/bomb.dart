import 'package:flame/components.dart';
import 'package:flutter/material.dart';
import 'package:flame/game.dart';

class Bomb extends PositionComponent with HasGameRef<FlameGame> {
  static const double bombSize = 40;
  static const double speed = 200; // pixels per second
  int lane = 1; // 0, 1, or 2
  final double roadLeft;
  final double laneWidth;

  SpriteComponent? spriteComponent;
  @override
  bool isLoaded = false;

  Bomb({
    required this.lane,
    required double y,
    required this.roadLeft,
    required this.laneWidth,
  }) {
    size = Vector2.all(bombSize);
    position = Vector2(_laneToX(lane), y);
  }

  double _laneToX(int lane) {
    return roadLeft + lane * laneWidth + (laneWidth - bombSize) / 2;
  }

  @override
  Future<void> onLoad() async {
    try {
      final sprite = await gameRef.loadSprite('bomb.png');
      spriteComponent = SpriteComponent(
        sprite: sprite,
        size: size,
        position: Vector2.zero(),
      );
      add(spriteComponent!);
      isLoaded = true;
    } catch (e) {
      // Fallback to red circle if image not found
      isLoaded = false;
    }
  }

  @override
  void render(Canvas canvas) {
    if (!isLoaded) {
      // Draw fallback red circle
      final paint = Paint()..color = Colors.red;
      canvas.drawCircle(Offset(size.x / 2, size.y / 2), size.x / 2, paint);
      final fusePaint = Paint()..color = Colors.orange;
      canvas.drawCircle(Offset(size.x / 2, size.y / 4), 6, fusePaint);
    }
    super.render(canvas);
  }

  @override
  void update(double dt) {
    super.update(dt);
    position.y += speed * dt;
  }

  void reset(int newLane, double y) {
    lane = newLane;
    position.x = _laneToX(lane);
    position.y = y;
  }
}
