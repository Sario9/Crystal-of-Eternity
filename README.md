﻿# Практика "Игра"
## Основное
- Название `Wizard’s dungeon: Crystal of Eternity`
- Платформа: `ПК`
- Технологии: `C#` `MonoGame`
- Язык: `Русский`
- Жанры: `2D` `Top-Down` `Action` `Dark fantasy` `Rogue-lite`
- Настроение: `Темное, мрачное`
- Сеттинг: `Фэнтезийное средневековье`
- Длительность игры: `10 минут`
- Главные игровые механики: `Необратимая смерть (PermaDeath)`, `Процедурная генерация `

## Сюжет
	Вы играете за отважного героя, который отправился на поиски легендарного артефакта - магического кристалла, который дает своему обладателю неограниченную власть. Кристалл был украден злым магом Медавом и скрыт в подземелье неподалеку от королевства. Вам предстоит пройти через различные опасности, сражаться с монстрами и боссами и улучшать свои навыки, чтобы наконец достичь цели.
	
	При входе в подземелье герой попадает в случайно сгенерированное подземелье, который каждый раз будет уникальным. Внутри лабиринта вы найдете различные предметы, такие как оружие, броня, артефакты, зелья и магические свитки, которые помогут вам в сражениях. Вы можете общаться с другими персонажами, получать задания и информацию, которая поможет вам продвинуться дальше. Ваша задача - найти кристалл, победить злого мага и вернуть мир в исходное состояние.


## Игровой мир
В игре присутствует несколько различных локаций, каждая из которых имеент своих уникальных противников.

Возможные варианты локаций и противников:
- Темный лес. Гоблины
- Руины храма. Живые статуи
- Заброшенная деревня. Бандиты и Волки
- Подземные катакомбы. Скелеты и Нежить
- Мрачный сад. Гноллы
- Река с бурлящими порогами. Крокодилы и Водные элементали
- Заброшенный замок. Рыцари-призраки и Полтергейсты

## Геймплей
Локация выбирается случайно.

Главный герой появляется в комнате, со случайным оружием, с помощью которого будет сражаться первое время.

Со всех сторон будут появляться враги, которые будут идти в сторону Воина. С некоторым шансом из поверженных врагов можно получить ресурсы и новое оружие. Новое оружие имеет случайные характеристики.

После прохождения комнаты будет открываться проход дальше.

### Комнаты
Виды
- `Обычные`
  - Необходимо победить N количество врагов
- `Специальные`
  - `Магазин` в котором можно приобрести снаряжение за ресурсы собранные с врагов
  - `Кузница` в которой можно перековать оружие для того чтобы повысить его характеристики
  - `Святилище` в котором можно восполнить здоровье. *Даже если здоровье полное, Воин получит бонус, игнорируя максимальный запас здоровья*
- `Арена`
  - Комната, в которой нужно сражаться с `Боссом`

Всего необходимо преодолеть 10 комнат:
- 1-3 комнаты - `Обычные`
- 4 комната - `Специальная`
- 5 комната - `Арена` Мини-босс
- 6-8 комнаты - `Обычные`
- 9 комната - `Специальная`
- 10 комната - `Арена` Финальный Босс


### Оружие
Виды оружия: `Меч` `Копьё` `Топор` `Лук` `Кинжал` `Булава` `Жезл` `Молот`

Характеристики оружия:
- `Урон`
  - Может быть `Физическим` и `Магическим`
- `Бронебойность`
- `Скорость атаки`
- `Дальность атаки`
- `Площадь атаки`
- `Скорость передвижения`