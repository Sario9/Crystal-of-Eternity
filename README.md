﻿# Практика "Игра"
## Основное
- Название `Wizard’s dungeon: Crystal of Eternity`
- Платформа: `ПК`
- Технологии: `C#` `MonoGame`
- Язык: `Русский`
- Жанры: `2D` `Top-Down` `Action` `Dark fantasy` `Rogue-lite`
- Настроение: `Темное, мрачное`
- Сеттинг: `Фэнтезийное средневековье`
- Длительность игры: `15 минут`
- Главные игровые механики: `Необратимая смерть (PermaDeath)`, `Процедурная генерация `

## Сюжет
Вы играете за отважного героя, который отправился на поиски легендарного артефакта - магического кристалла, который дает своему обладателю неограниченную власть. Кристалл был украден злым магом Медавом и скрыт в подземелье неподалеку от королевства. Вам предстоит пройти через различные опасности, сражаться с монстрами и боссами и улучшать свои навыки, чтобы наконец достичь цели.
	
При входе в подземелье герой попадает в случайно сгенерированное подземелье, который каждый раз будет уникальным. Внутри лабиринта вы найдете различные предметы, такие как оружие, броня, артефакты, зелья и магические свитки, которые помогут вам в сражениях. Вы можете общаться с другими персонажами, получать задания и информацию, которая поможет вам продвинуться дальше. Ваша задача - найти кристалл, победить злого мага и вернуть мир в исходное состояние.


## Игровой мир:
### Описание мира:
- Существует 4 королевства:
  - `Красное` - королевство нашего главного героя, население в основном люди, около 10% - эльфы, и 5% - другие расы, находится в состоянии войны с зеленым королевством
  - `Синее` - королевство чародеев, из которого и пришел колдун, укравший кристалл вечности, население состоит на 100% из эльфов, остальные расы истребляются
  - `Зеленое` - королевство гоблинов, король решил захватить Красное королевство и завладеть его богатствами, население 80% - гоблины, 15% - зверолюди, остальные - другие расы
  - `Желтое` - королевство зверолюдей, самое богатое и могущественное среди остальных, 90% населения - зверолюди и остальных понемногу
- `Главный герой` - человек, который помнит только то, как проснулся в тронном зале Красного королевства и увидел, что его посвятили в защитники королевства, и теперь он должен отправится в подземелье и вернуть украденный кристалл. Неизвестно почему умеет обращаться с любым видом оружия и использовать магию из книг и свитков.
- `Святой свет` - Главная вера мира, Зимар - отец всего, прислушивается к молитвам и раз в столетие приходит в мир смертных и выбирает королевство, которое получит благословение до его следующего визита. Оно избавляет все население от болезней, делает его более способным и талантливым. В данный момент благословлено королевство Зверолюдей
Существуют также `культы`, которые противятся Зимару и проводят различные ритуалы для усиления своих тел и призыва демонов из преисподней
- `Злой маг Медав` - выходец из Синего королевства, состоит в культе Амории, владычицы загробного мира, который известен благодаря своим некромантам и превосходным колдунам. Захотел стать самым могущественным существом в Мире и убить Зимара, поэтому и выкрал кристалл из сокровищницы Красного королевства.
- `Кристалл вечности` - легендарный артефакт древности, дарованный Красному королевству Зимаром 4 столетия назад. Владея этим кристаллом можно заполучить невероятные силы, которые зависят от его владельца. Предыдущим владельцем артефакта был король Красного королевства, который защитил весь мир от вторжения демонов, исчез при неизвестных обстоятельствах вместе с кристаллом.
- `Подземелье колдуна` - по слухам место, где захоронено тело Зимара, в самой глубине которого находится убежище Медава. Подземелье постоянно изменяется под действием силы кристалла, поэтому еще никому не удалось составить его карту. Убежище доверху наполнено ловушками и различными существами, среди которых скелеты, которых воскресил колдун, слизни(никто не знает откуда они берутся), разбойники и гоблины, пришедшие в поисках сокровищ.

### Описание локаций
- `Заброшенная тропа` - очень красивое место, изменившееся после прихода колдуна до неузнаваемости,
чем ближе ко входу в подземелье, тем сильнее перемены. На тропе можно встретить слизней,
слабых скелетов, зомби, гоблинов и разбойников.
- `Верхний уровень` - первый этаж подземелья, видно следы множества схваток, обитает множество зомби и скелетов, а также можно встретить бесов.
- `Гробница` - место захоронения тела Зимара, сюда могли дойти только сильные авантюристы и разбойники, поэтому зомби и скелеты в этом месте сильнее тех, что на верхнем уровне, некоторые обладают магией, также можно встретить несколько низкоуровневых демонов, представляющих немалую опасность
- `Недра подземелья` - последний уровень подземелья, где скрывается Медав вместе с кристаллом вечности, как и ранее можно встретить скелетов и низкоуровневых демонов, но здесь обитают и высокоуровневые демоны, чтобы победить которых понадобится немалое усилие

## Геймплей
В начале игры и после смерти персонаж попадает в замок, в котором он может общаться с npc,
улучшать навыки или менять свое основное снаряжение. При выходе из замка игрок отправляется в подземелье,
которое состоит из 4 локаций, которые в свою очередь делятся на 4 уровня

При входе в комнату будут появляться противники, которые автоматически начнут идти на игрока со всех сторон и атаковать его.
Игрок может использовать все имеющееся у него снаряжение для убийства врагов и завершения уровня.
После прохождения комнаты будет открываться проход дальше.

### Комнаты
Виды
- `Обычные`
  - Необходимо победить всех враго в комнате
- `Специальные`
  - `Таинственный магазин` в котором можно приобрести снаряжение и артефакты за ресурсы собранные с врагов
  - `Колодец жизни` в котором можно восполнить здоровье или вместо этого повысить максимальное здоровье до конца забега.
- `Арена`
  - Последняя комната локации, в которой нужно сражаться с `Боссом`

Всего необходимо пройти 4 локации, которые состоят из 4 комнат:
- 1-2 комнаты - `Обычные`
- 3 комната - `Специальная`
- 4 комната - `Арена`

### Сущности
`Разрушаемые объекты`
- Ящики
- Горшки
- Черепа

`Враги`
- Скелеты
- Слизни
- Гоблины
- Разбойники
- Демоны
- Боссы

`Игрок`

### Боссы
- `Бандит-главарь`
- `Сгоревший скелет`
- `Хранитель гробницы`
- `Злой маг Медав` (Возможно будет меняться предыдущем персонажем игрока в NG+)

### NPC
- `Кузнец Брон` - мастер своего ремесла и один из главных персонажей в игре. Он является не только искусным кузнецом, но и мастером боя, который может создавать самые лучшие оружия для игрока, а также позволяет игроку менять класс. Также он может помогать игроку в выборе оружия и давать ценные советы, как справиться с трудными ситуациями в бою. Это не просто персонаж, а важный игровой элемент, который может существенно повлиять на опыт игрока и помочь ему преодолеть трудности в игре.
- `Король Роберт` - правитель Красного королевства, поручил герою вернуть кристалл из подземелья. Он предоставляет герою полезные советы и дает задания во время его путешествия
- `Мастер Шань` - опытный наставник и мастер боевых искусств. Предоставляет герою доступ к древу талантов, которое позволяет ему улучшать свои навыки в зависимости от его предпочтений и стиля игры. Он также может дать герою новые навыки, которые помогут ему справиться с новыми вызовами, которые могут возникнуть во время его путешествия.
- `Таинственный торговец` - загадочный персонаж, который неизвестно каким образом попадает в подземелье и предлагает игроку свои товары. Он никогда не раскрывает своего истинного имени и происхождения, но его ассортимент всегда широк. Он может предложить игроку зелья, магические свитки, артефакты и другие предметы, которые могут помочь в бою и в исследовании подземелья

### Расходники:
- `Зелья`
  - Здоровья(50%)
  - Маны(70%)
  - Повышения урона(10%)
  - Повышения защиты(20%)
  - Невидимости(10 сек, снятие при нанесении/получении урона)
  - Стойкости(Поглощения 1 попадания)

- `Еда:`
  - `Яблоко` (5% HP)
  - `Хлеб` (10% HP)
  - `Яичница` 15% HP)
  - `Курица` (20% HP)
  - `Стейк` (30% HP)
  - `Вода` (10% MP)
  - `Манго` (20% MP)
  - `Драгонфрут` (30% MP)

- `Ключи:`
  - От Сундуков: `Золотые`, `Серебряные`
  - От дверей

`Амуниция:`
- Стрелы


### Оружие
Виды оружия: `Меч` `Копьё` `Топор` `Лук` `Арбалет` `Магический жезл`

Характеристики оружия:
- `Урон`
  - `Физический`
  - `Магический`
- `Скорость атаки`
- `Дальность атаки`
- `Угол атаки`

## Дополнительные механики:
- Процедурная генерация локаций
- Возможность смены класса(влияет на древо навыков и уникальную способность)
- Диалоги с NPC
- Квесты
- Опыт персонажа
- Древо навыков персонажа
- Возможность открытия нового оружия у NPC “Кузнец Брон”
- Рандомизация свойств предметов, найденных в подземелье
- Инвентарь(Слоты брони, оружия, артефактов и место для расходников)
- Еда(Восполняет здоровье и/или ману) и зелья лечения
- Магазин в лабиринте
- Рывок(Дает неуязвимость на N секунд и резко перемещает персонажа на небольшое расстояние в направлении движения (При применении тратятся заряды, которые восстанавливаются за определенное время после траты));
- Таблица со статистикой игрока за все время
