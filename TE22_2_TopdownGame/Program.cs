using Raylib_cs;
using System.Numerics;

// string enemyName;

// List<string> names = new() { "Micke", "Martin", "Lena" };
// names.Add("Christian");
// names.Insert(1, "Herbert");

// foreach (string name in names)
// {
//   Console.WriteLine(name);
// }

// for (int j = 0; j < names.Count; j++)
// {
//   Console.WriteLine(names[j]);
// }

// int i = 0;
// while (i < names.Count)
// {
//   Console.WriteLine(names[i]);
//   i++;
// }


// int i = Random.Shared.Next(names.Count);
// Console.WriteLine(names[i]);

// Console.ReadLine();


// int i = generator.Next(3);
// if (i == 0)
// {
//   enemyName = "Micke";
// }
// else if (i == 1)
// {
//   enemyName = "Martin";
// }
// else if (i == 2)
// {
//   enemyName = "Lena";
// }




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

int floorY = 550;
int floorSpeedY = -1;
Vector2 movement = new Vector2(0.1f, 0.1f);
Color hotPink = new Color(255, 105, 180, 255);


Texture2D characterImage = Raylib.LoadTexture("imse.png");
Rectangle characterRect = new Rectangle(100, 100, 32, 32);
characterRect.width = characterImage.width;
characterRect.height = characterImage.height;

List<Rectangle> walls = new();

// for (int x = 0; x < 800; x += 32)
// {
//   for (int y = 0; y < 600; y += 32)
//   {
//     if (x % 64 == 0)
//     {

//       walls.Add(new Rectangle(x, y, 16, 16));
//     }
//   }
// }

walls.Add(new Rectangle(32, 32, 32, 128));
walls.Add(new Rectangle(64, 32, 128, 32));
walls.Add(new Rectangle(192, 32, 32, 128));
walls.Add(new Rectangle(256, 32, 32, 128));

Rectangle doorRect = new Rectangle(760, 460, 32, 32);

float speed = 5;

string scene = "start";

int points = 0;

while (!Raylib.WindowShouldClose())
{
  // --------------------------------------------------------------------------
  // GAME LOGIC
  // --------------------------------------------------------------------------

  if (scene == "start")
  {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
    {
      scene = "game";
    }
  }
  else if (scene == "game")
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
      movement = Vector2.Normalize(movement) * speed;
    }

    characterRect.x += movement.X;

    bool isInAWall = CheckIfWall(characterRect, walls);

    if (isInAWall == true)
    {
      characterRect.x -= movement.X;
    }

    characterRect.y += movement.Y;
    
    isInAWall = CheckIfWall(characterRect, walls);
    if (isInAWall == true)
    {
      characterRect.y -= movement.Y;
    }

    // if (characterRect.x < 0 || characterRect.x > 800 - 64)
    // {
    //   characterRect.x -= movement.X;
    // }
    // if (characterRect.y < 0 || characterRect.y > 600 - 64)
    // {
    //   characterRect.y -= movement.Y;
    // }

    if (Raylib.CheckCollisionRecs(characterRect, doorRect))
    {
      points++;
    }


  }

  // --------------------------------------------------------------------------
  // RENDERING
  // --------------------------------------------------------------------------

  Raylib.BeginDrawing();
  if (scene == "start")
  {
    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.DrawText("Press SPACE to start", 10, 10, 32, Color.BLACK);
  }
  else if (scene == "game")
  {
    Raylib.ClearBackground(Color.GOLD);

    Raylib.DrawRectangle(0, floorY, 800, 30, Color.BLACK);

    Raylib.DrawRectangleRec(characterRect, Color.BLUE);
    Raylib.DrawTexture(characterImage, (int)characterRect.x, (int)characterRect.y, Color.WHITE);

    Raylib.DrawRectangleRec(doorRect, Color.BROWN);

    foreach (Rectangle wall in walls)
    {
      Raylib.DrawRectangleRec(wall, Color.BLACK);
    }

    Raylib.DrawText($"Points: {points}", 10, 10, 32, Color.WHITE);

  }

  Raylib.EndDrawing();
}

static bool CheckIfWall(Rectangle characterRect, List<Rectangle> walls)
{
  foreach (Rectangle wall in walls)
  {
    if (Raylib.CheckCollisionRecs(characterRect, wall))
    {
      return true;
    }
  }
  return false;
}