using System;
using System.Collections.Generic;

Random random = new Random(); 
List<string> monstersNames = new List<string> { "феи", "мутировавшие псы", "огры", "босс" };
List<int> monstersDamages = new List<int> { 15, 25, 30, 50 };

int playerhp = random.Next(100, 200); // здоровье нашего героя
int money = 0;   // деньги
int potion = 1;  // зелье
int weapon = 0;  // оружие
string fists = "Кулаки";

Console.WriteLine("Добро пожаловать в игру ТРОПА МОНСТРОВ!");
Console.WriteLine("ВЫ ПРОСТОЙ ОБЫВАТЕЛЬ В СТРАНЕ МАГОВ И ФЕЙ, СПАСИТЕ ВАШУ ДЕРЕВНЮ ОТ ЗЛЫХ МОНСТРОВ!");
Console.Write("\tНАЗОВИТЕ ВАШЕГО ПЕРСОНАЖА: ");
string? Name = Console.ReadLine();
Console.Clear();

// ГЛАВНЫЙ ЦИКЛ ИГРЫ
for (int level = 1; level <= 4; level++)  
{
    int index = level - 1;
    int monstersHp = random.Next(30, 60) * level;
    Console.WriteLine($"\n\t### УРОВЕНЬ: {level} ###");
    Console.WriteLine($"ВЫ ВСТРЕТИЛИ ВРАГА: {monstersNames[index]} ОН ВАС АТАКУЕТ!");
  
    // ЦИКЛ БОЯ
    while (monstersHp > 0 && playerhp > 0)
    {
        Console.WriteLine($"\n{Name} HP: {playerhp} | ОРУЖИЕ: {fists} (+{weapon}) | ЗЕЛЬЕ: {potion}");
        Console.WriteLine($"{monstersNames[index]} НР: {monstersHp}");
        Console.WriteLine("ВЫБЕРИТЕ ДЕЙСТВИЕ: 1 — АТАКОВАТЬ | 2 — ВЫПИТЬ ЗЕЛЬЕ (+40 HP)");
        Console.Write("ВАШ ВЫБОР: ");
        string? result = Console.ReadLine();

        if (result == "2")
        {
            if (potion > 0)
            {
                potion--;
                playerhp += 40;
                Console.WriteLine($"Вы выпили зелье! Здоровье восстановлено. Текущее HP: {playerhp}");
            }
            else
            {
                Console.WriteLine("У ВАС НЕТ ЗЕЛИЙ!");
                result = "1"; 
            }
        }

        if (result == "1" || result != "2")
        {
            int damageplayer = random.Next(10, 35) + weapon;
            monstersHp -= damageplayer;
            Console.WriteLine($"Вы нанесли урон: {damageplayer}! Осталось здоровья врага: {Math.Max(0, monstersHp)}");
        }

        // Ход монстра
        if (monstersHp > 0)
        {
            playerhp -= monstersDamages[index];
            Console.WriteLine($"{monstersNames[index]} нанес вам урон: {monstersDamages[index]}!");
        }
        
        // Проверка смерти и побега
        if (playerhp <= 0)
        {
            Console.WriteLine("\nВЫ ПРИСМЕРТИ! ПОПРОБОВАТЬ СБЕЖАТЬ В ДЕРЕВНЮ?! (ШАНС 30%)");
            Console.WriteLine("ПОПРОБОВАТЬ СБЕЖАТЬ - 1 | ПРИНЯТЬ СУДЬБУ - 2");
            Console.Write("ВАШ ВЫБОР: ");
            string? exitplayer = Console.ReadLine();

            if (exitplayer == "1" && random.Next(1, 101) <= 30)
            {
                playerhp = 20;
                money = 0;
                Console.WriteLine($"\tПОЗДРАВЛЯЮ! ВЫ СМОГЛИ СБЕЖАТЬ В ДЕРЕВНЮ! HP: {playerhp}, НО ВСЕ ДЕНЬГИ ПОТЕРЯНЫ.");
                break; 
            }
            else
            {
                Console.WriteLine($"\tВЫ ПОГИБЛИ В БОЮ! ВАС УБИЛ {monstersNames[index]}");
                break; 
            }
        }
    } // Конец цикла боя while

    // Если игрок погиб окончательно — прерываем игру
    if (playerhp <= 0) break;

    // НАГРАДА ЗА ПОБЕДУ
    int lootMoney = random.Next(15, 31);
    money += lootMoney;
    Console.WriteLine($"\nПОЗДРАВЛЯЮ, ВЫ ПОБЕДИЛИ!");
    Console.WriteLine($"С него вам выпало немного монет: +{lootMoney} золота!");
    
    if (random.Next(1, 101) <= 40)
    {
        potion++;
        Console.WriteLine("Вы также обыскали монстра и нашли Зелье Здоровья!");
    }
    Console.WriteLine($"УРОВЕНЬ ПРОЙДЕН. ВАШИ МОНЕТЫ: {money}");

    // ВЫЗОВ МЕНЮ ОТДЫХА
    if (level < 4 && playerhp > 0)
    {
        Menu(ref money, ref potion, ref weapon, ref fists, ref playerhp, level + 1);
    }

    Console.WriteLine("\nНажмите Enter для перехода дальше...");
    Console.ReadLine();
    Console.Clear();
} // Конец цикла уровней for

// ФИНАЛ ИГРЫ
if (playerhp > 0)
{
    Console.WriteLine($"\n========================================");
    Console.WriteLine($"ПОЗДРАВЛЯЕМ, {Name}! ВЫ ПРОШЛИ ВСЮ ИГРУ И СПАСЛИ ДЕРЕВНЮ!");
    Console.WriteLine($"Накопленные вами деньги составляют: {money} монет.");
    Console.WriteLine($"Ваше финальное оружие: {fists} (+{weapon} к урону)");
    Console.WriteLine($"========================================");
}

// =====================================================================
// МЕТОДЫ ИГРЫ
// =====================================================================

static void Menu(ref int money, ref int potion, ref int weapon, ref string fists, ref int playerhp, int nextlevel)
{
    bool startMenuPlayer = true;
    while (startMenuPlayer)
    {
        Console.Clear();
        Console.WriteLine($"\n=== ЛАГЕРЬ ОТДЫХА. ПУТЬ НА {nextlevel} УРОВЕНЬ ===");
        Console.WriteLine("1 - Посмотреть характеристики персонажа");
        Console.WriteLine("2 - Выпить зелье здоровья (+40 HP)");
        Console.WriteLine("3 - Открыть магазин торговца");
        Console.WriteLine("4 - Продолжить путь (Перейти на следующий уровень)");
        Console.Write("Ваш выбор: ");

        string? enter = Console.ReadLine();
        switch (enter)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("=== ВАШИ ХАРАКТЕРИСТИКИ ===");
                Console.WriteLine($"Здоровье: {playerhp} HP");
                Console.WriteLine($"Золото: {money} монет");
                Console.WriteLine($"Зелья в инвентаре: {potion}");
                Console.WriteLine($"Оружие: {fists} (+{weapon} к урону)");
                Console.WriteLine("\nНажмите Enter, чтобы вернуться в лагерь...");
                Console.ReadLine();
                break;
        
            case "2":
                Console.Clear();
                if (potion > 0)
                {
                    potion--;
                    playerhp += 40;
                    Console.WriteLine($"Вы выпили зелье! Здоровье восстановлено. Теперь у вас: {playerhp} HP.");
                }
                else
                {
                    Console.WriteLine("У вас закончились зелья! Купите их у торговца.");
                }
                Console.WriteLine("\nНажмите Enter, чтобы вернуться в лагерь...");
                Console.ReadLine();
                break;
        
            case "3":
                Shop(ref money, ref potion, ref weapon, ref fists, nextlevel);
                break;
            
            case "4":
                Console.WriteLine("Вы потушили костер и пошли дальше в свою деревню сражаться с монстрами! Удачи");
                startMenuPlayer = false;
                break;

            default:
                Console.WriteLine("НЕВЕРНЫЙ ВЫБОР, ПОПРОБУЙТЕ ЕЩЕ РАЗ!");
                Console.ReadLine();
                break;
        }
    }
}

static void Shop(ref int money, ref int potion, ref int weapon, ref string fists, int nextlevel)
{
    bool shopping = true;
    while (shopping)
    {
        Console.Clear();
        Console.WriteLine($"\n--- ТОРГОВЕЦ ПО ПУТИ НА {nextlevel} УРОВЕНЬ ---");
        Console.WriteLine($"Ваше золото: {money} монет | Зелий: {potion} | Оружие: {fists} (+{weapon})");
        Console.WriteLine("Что желаете приобрести?");
        Console.WriteLine("1 - Купить зелье здоровья (Цена: 15 монет)");
        
        if (weapon < 15)
        {
            Console.WriteLine("2 - Купить Охотничий нож (+15 к урону) (Цена: 25 монет)");
        }
        else if (weapon < 35)
        {
            Console.WriteLine("2 - Buy Меч Мага (+35 к урону) (Цена: 50 монет)");
        }
        else
        {
            Console.WriteLine("2 - [Распродано] Лучшее оружие уже у вас!");
        }

        Console.WriteLine("3 - Назад в лагерь");
        Console.Write("Выбор: ");
        string? shopChoice = Console.ReadLine();

        if (shopChoice == "1")
        {
            if (money >= 15)
            {
                money -= 15;
                potion++;
                Console.WriteLine("Вы купили зелье здоровья!");
            }
            else
            {
                Console.WriteLine("Недостаточно монет для зелья!");
            }
            Console.ReadLine();
        } 
        else if (shopChoice == "2")
        {
            if (weapon < 15)
            {
                if (money >= 25)
                {
                    money -= 25;
                    weapon = 15;
                    fists = "Охотничий нож";
                    Console.WriteLine("Вы экипировали Охотничий нож!");
                }
                else Console.WriteLine("Недостаточно монет на нож!");   
            }
            else if (weapon < 35)
            {
                if (money >= 50)
                {
                    money -= 50;
                    weapon = 35;
                    fists = "Меч Мага";
                    Console.WriteLine("Вы экипировали Меч Мага!");
                }
                else Console.WriteLine("Недостаточно монет на Меч Мага!");
            }
            Console.ReadLine();
        }
        else if (shopChoice == "3")
        {
            shopping = false;  
        }
        else
        {
            Console.WriteLine("Неверный ввод, попробуйте снова.");
            Console.ReadLine();
        }
    }
}
