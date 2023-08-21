using System;
using System.Numerics;
using Raylib_cs;

namespace 桌面宠物;

class Program
{
    static void Main(string[] args)
    {
        float instantSecond = 0;
        int instantFrame = 0;
        float speed = 0.05f;

        bool isVocal = false;
        float vocalInstantSecond = 0;
        float vocalSpeed = 1.25f;
        Random random = new Random();

        Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_TRANSPARENT);
        Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_UNDECORATED);

        Raylib.InitWindow(1000/4, 800/4, "重岳");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        Image[] images = new Image[4];
        Texture2D[] texture2Ds = new Texture2D[4];

        for (int i = 0; i < images.Length; i++)
        {
            images[i] = Raylib.LoadImage("shuo/" + i + ".png");
            texture2Ds[i] = Raylib.LoadTextureFromImage(images[i]);
        }

        Sound[] sounds = new Sound[4];
        Sound playing = new Sound();

        sounds[0] = Raylib.LoadSound("vocal/形不成形" + ".wav");
        sounds[1] = Raylib.LoadSound("vocal/千招百式" + ".wav");
        sounds[2] = Raylib.LoadSound("vocal/劲发江潮落" + ".wav");
        sounds[3] = Raylib.LoadSound("vocal/你们解决问题" + ".wav");

        Raylib.SetWindowPosition(Raylib.GetMonitorWidth(0) - Raylib.GetScreenWidth(), Raylib.GetMonitorHeight(0) - Raylib.GetScreenHeight());

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib .IsKeyDown(KeyboardKey.KEY_LEFT))
                Raylib.SetWindowPosition((int)Raylib.GetWindowPosition().X - 5, (int)Raylib.GetWindowPosition().Y);
            if (Raylib .IsKeyDown(KeyboardKey.KEY_RIGHT))
                Raylib.SetWindowPosition((int)Raylib.GetWindowPosition().X + 5, (int)Raylib.GetWindowPosition().Y);
            if (Raylib .IsKeyDown(KeyboardKey.KEY_UP))
                Raylib.SetWindowPosition((int)Raylib.GetWindowPosition().X, (int)Raylib.GetWindowPosition().Y - 5);
            if (Raylib .IsKeyDown(KeyboardKey.KEY_DOWN))
                Raylib.SetWindowPosition((int)Raylib.GetWindowPosition().X, (int)Raylib.GetWindowPosition().Y + 5);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_Y))
                Raylib.SetWindowPosition(0, 0);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_U))
                Raylib.SetWindowPosition(Raylib.GetMonitorWidth(0) - Raylib.GetScreenWidth(), 0);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_H))
                Raylib.SetWindowPosition(0, Raylib.GetMonitorHeight(0) - Raylib.GetScreenHeight());
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_J))
                Raylib.SetWindowPosition(Raylib.GetMonitorWidth(0) - Raylib.GetScreenWidth(), Raylib.GetMonitorHeight(0) - Raylib.GetScreenHeight());
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                Raylib.SetWindowPosition((Raylib.GetMonitorWidth(0) - Raylib.GetScreenWidth()) / 2, (Raylib.GetMonitorHeight(0) - Raylib.GetScreenHeight()) / 2);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_Q))
            {
                Raylib.StopSound(playing);
                playing = sounds[0];
                Raylib.PlaySound(playing);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
            {
                Raylib.StopSound(playing);
                playing = sounds[1];
                Raylib.PlaySound(playing);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_E))
            {
                Raylib.StopSound(playing);
                playing = sounds[2];
                Raylib.PlaySound(playing);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                Raylib.StopSound(playing);
                playing = sounds[3];
                Raylib.PlaySound(playing);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_V))
            {
                isVocal = !isVocal;
            }
            if (isVocal)
            {
                vocalInstantSecond += Raylib.GetFrameTime();
                if (vocalInstantSecond>vocalSpeed)
                {
                    Raylib.StopSound(playing);
                    playing = sounds[random.Next(4)];
                    Raylib.PlaySound(playing);

                    vocalInstantSecond -= vocalSpeed;
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(0, 0, 0, 0));

            instantSecond += Raylib.GetFrameTime();
            if (instantSecond > speed)
            {
                instantFrame = (instantFrame + 1) % 4;
                instantSecond -= speed;
	        }
            Raylib.DrawTextureEx(texture2Ds[instantFrame], new Vector2(0, 0), 0, 0.25f, new Color(255, 255, 255, 255));
            Raylib.EndDrawing();
        }
        for (int i = 0; i < 4; i++)
        {
            Raylib.UnloadImage(images[i]);
            Raylib.UnloadTexture(texture2Ds[i]);
        }
        Raylib.CloseWindow();
        Raylib.CloseAudioDevice();
    }
}

