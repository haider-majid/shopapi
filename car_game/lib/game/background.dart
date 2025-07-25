import 'package:flame/components.dart';
import 'package:flutter/material.dart';

class Background extends PositionComponent {
  static const double roadWidth = 240; // 3 lanes, 80px each
  static const double laneWidth = 80;
  static const double dividerWidth = 4;
  static const double dividerHeight = 40;
  static const double dividerGap = 40;
  static const double roadEdgePadding = 20;
  double scroll = 0;
  static const double scrollSpeed = 150; // pixels per second

  @override
  void render(Canvas canvas) {
    super.render(canvas);
    final size = this.size;
    final roadLeft = (size.x - roadWidth) / 2;
    final roadRight = roadLeft + roadWidth;
    final roadRect = Rect.fromLTWH(roadLeft, 0, roadWidth, size.y);
    // Draw grass/garden
    final grassPaint = Paint()..color = Colors.green[700]!;
    canvas.drawRect(Rect.fromLTWH(0, 0, roadLeft, size.y), grassPaint);
    canvas.drawRect(
      Rect.fromLTWH(roadRight, 0, size.x - roadRight, size.y),
      grassPaint,
    );
    // Draw road
    final roadPaint = Paint()..color = Colors.grey[800]!;
    canvas.drawRect(roadRect, roadPaint);
    // Draw lane dividers
    final dividerPaint = Paint()
      ..color = Colors.white
      ..strokeWidth = dividerWidth;
    for (int lane = 1; lane < 3; lane++) {
      final x = roadLeft + lane * laneWidth;
      double y = -dividerHeight + (scroll % (dividerHeight + dividerGap));
      while (y < size.y) {
        canvas.drawLine(
          Offset(x, y),
          Offset(x, y + dividerHeight),
          dividerPaint,
        );
        y += dividerHeight + dividerGap;
      }
    }
    // Draw road edges
    final edgePaint = Paint()
      ..color = Colors.yellow
      ..strokeWidth = dividerWidth;
    canvas.drawLine(Offset(roadLeft, 0), Offset(roadLeft, size.y), edgePaint);
    canvas.drawLine(Offset(roadRight, 0), Offset(roadRight, size.y), edgePaint);
  }

  @override
  void update(double dt) {
    scroll += scrollSpeed * dt;
    if (scroll > (dividerHeight + dividerGap)) {
      scroll -= (dividerHeight + dividerGap);
    }
  }
}
