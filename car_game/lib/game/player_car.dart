import 'package:flame/components.dart';
import 'package:flame/game.dart';

class PlayerCar extends PositionComponent with HasGameRef<FlameGame> {
  static const double carWidth = 50;
  static const double carHeight = 100;
  static const double moveSpeed = 300; // pixels per second

  final double roadLeft;
  final double laneWidth;

  late SpriteComponent spriteComponent;
  @override
  bool isLoaded = false;

  PlayerCar({required this.roadLeft, required this.laneWidth});

  @override
  Future<void> onLoad() async {
    size = Vector2(carWidth, carHeight);
    // Start at bottom center of the road
    position = Vector2(
      roadLeft + (laneWidth * 1.5) - (carWidth / 2),
      gameRef.size.y - carHeight - 20,
    );
    final sprite = await gameRef.loadSprite('car.png');
    spriteComponent = SpriteComponent(
      sprite: sprite,
      size: size,
      position: Vector2.zero(),
    );
    add(spriteComponent);
    isLoaded = true;
  }

  void moveLeft(double dt) {
    position.x -= moveSpeed * dt;
    if (position.x < roadLeft) position.x = roadLeft;
  }

  void moveRight(double dt) {
    position.x += moveSpeed * dt;
    if (position.x > roadLeft + laneWidth * 3 - carWidth) {
      position.x = roadLeft + laneWidth * 3 - carWidth;
    }
  }
}
