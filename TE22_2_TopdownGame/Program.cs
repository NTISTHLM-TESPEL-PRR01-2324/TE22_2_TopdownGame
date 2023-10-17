﻿using Raylib_cs;
using System.Numerics;

// - Vektorer för förflyttning
// - Vektorer för positioner (cirklar)
// - Rektanglar
// - Texturer
// - Färger

// - Input
// - Olika scener
// - Kollisioner

Raylib.InitWindow(800, 600, "Hello");
Raylib.SetTargetFPS(60);

// int x = 0;
int floorY = 550;
int floorSpeedY = -1;
// Vector2 position = new Vector2(0, 0);
Vector2 movement = new Vector2(0.1f, 0.1f);

Color hotPink = new Color(255, 105, 180, 255);


Texture2D characterImage = Raylib.LoadTexture("imse.png");
Rectangle characterRect = new Rectangle(10, 10, 32, 32);
characterRect.width = characterImage.width;
characterRect.height = characterImage.height;

float speed = 5;

while (!Raylib.WindowShouldClose())
{
  movement = Vector2.Zero;

  // kod här: läsa in knapptryck, ändra på movement
  if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
  {
    movement.X = -1;
  }
  else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
  {
    movement.X = 1;
  }
  if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
  {
    movement.Y = -1;
  }
  else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
  {
    movement.Y = 1;
  }

  if (movement.Length() > 0)
  {
    movement = Vector2.Normalize(movement);
  }

  movement *= speed;

  characterRect.x += movement.X;
  characterRect.y += movement.Y;

  // x++;
  floorY += floorSpeedY;
  if (floorY < 0)
  {
    floorSpeedY = 1;
  }
  else if (floorY > 550)
  {
    floorSpeedY = -1;
  }

  // position += movement;

  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.GOLD);


  // Raylib.DrawCircle(x, 300, 30, Color.BLACK);
  // Raylib.DrawCircleV(position, 20, hotPink);

  Raylib.DrawRectangle(0, floorY, 800, 30, Color.BLACK);

  Raylib.DrawRectangleRec(characterRect, Color.BLUE);
  Raylib.DrawTexture(characterImage, (int) characterRect.x, (int) characterRect.y, Color.WHITE);

  Raylib.EndDrawing();
}
