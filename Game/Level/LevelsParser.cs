using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crystal_of_Eternity
{
    public static class LevelsParser
    {
        public static Level ParseFromFile(string fileName)
        {
            var filePath = @$"C:\Users\necro\OneDrive\Рабочий стол\Урфу\2 семестр\Прога\Crystal of Eternity\Levels\{fileName}.txt";

            var sr = new StreamReader(filePath);
            var lines = sr
                .ReadToEnd()
                .Split('\n')
                .Select(x => x.Trim())
                .Select(x => x.ToLower())
                .ToArray();
            sr.Close();
            var levelType = lines[0] switch
            {
                "level_1" => LevelType.Level1,
                "level_2" => LevelType.Level2,
                _ => throw new ArgumentException("Неверный уровень"),
            };

            var rooms = new List<Room>();
            foreach (var line in lines)
            {
                if (line == lines[0]) continue;
                rooms.Add(ParseRoom(levelType, line));
            }

            return new(levelType, rooms);
        }

        private static Room ParseRoom(LevelType levelType, string line)
        {
            line = line.ToLower();
            var split1 = line.Split(':');
            var roomName = split1[0];
            var roomParams = split1[1]
                .Split()
                .Select(x => x.Trim()).Where(x => x != "")
                .ToArray();
            var roomType = roomParams[0];
            Room room = roomType switch
            {
                "default" => GenerateDefaultRoom(levelType, roomParams),
                "special" => GenerateSpecialRoom(levelType, roomParams),
                _ => throw new ArgumentException("Неверный тип комнаты"),
            };
            return room;
        }

        private static DefaultRoom GenerateDefaultRoom(LevelType level, string[] parameters)
        {
            
            var size = ParseVector2(parameters[1]).ToPoint();
            var playerStartPosition = ParseVector2(parameters[2]);
            var totalEnemies = int.Parse(parameters[3]);
            var enemies = new List<Enemy>();
            for (int i = 4; i < parameters.Length; i++)
            {
                var text = parameters[i];
                switch (text)
                {
                    case "skeleton":
                        enemies.Add(new Skeleton());
                        break;
                    case "rogue_1":
                        enemies.Add(new Rogue(1));
                        break;
                    case "rogue_2":
                        enemies.Add(new Rogue(2));
                        break;
                    case "rogue_3":
                        enemies.Add(new Rogue(3));
                        break;
                    case "demon":
                        enemies.Add(new Demon());
                        break;
                    default:
                        throw new ArgumentException("Враг отсутствует");
                }
            }

            return new(level, size, playerStartPosition, totalEnemies, enemies);
        }

        private static SpecialRoom GenerateSpecialRoom(LevelType level, string[] parameters)
        {
            var playerStartPosition = ParseVector2(parameters[1]);
            SpecialRoomType specialRoomType;
            switch(parameters[2])
            {
                case "fountain":
                    specialRoomType = SpecialRoomType.Fountain;
                    break;
                case "shop":
                    specialRoomType= SpecialRoomType.Shop;
                    break;
                default:
                    throw new ArgumentException();
            }

            return new(level, playerStartPosition, specialRoomType);
        }

        private static Vector2 ParseVector2(string text)
        {
            var split = text.Split(',').Select(x => x.Trim(' ', '(', ')')).Where(x => x != "").Select(x => int.Parse(x)).ToArray();
            return new(split[0], split[1]);
        }
    }
}
