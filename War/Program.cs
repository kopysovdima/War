using System;
using System.Collections.Generic;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            PlatoonCreater platoonCreator = new PlatoonCreater();
            Platoon platoonCountryRed = platoonCreator.Create(5);
            Platoon platoonCountryBlue = platoonCreator.Create(5);

            Battlefield battlefield = new Battlefield();
            battlefield.Work(platoonCountryRed, platoonCountryBlue);
        }
    }

    class Battlefield
    {
        public void Work(Platoon platoonCountryRed, Platoon platoonCountryBlue)
        {
            Console.WriteLine("Война между взводами двух стран, краной и синей");
            Console.ReadKey();
            Battle(platoonCountryRed, platoonCountryBlue);
            ShowBattleResult(platoonCountryRed, platoonCountryBlue);
        }

        public void Battle(Platoon platoonCountryRed, Platoon platoonCountryBlue)
        {

            while (platoonCountryRed.Count > 0 && platoonCountryBlue.Count > 0)
            {
                Soldier firstSolider = platoonCountryRed.GetSoldier();
                Soldier secondSolider = platoonCountryBlue.GetSoldier();

                Console.WriteLine("Красный взвод:");
                platoonCountryRed.ShowInfo();
                Console.WriteLine("Синий взвод:");
                platoonCountryBlue.ShowInfo();

                firstSolider.Takedamage(secondSolider.Damage);
                secondSolider.Takedamage(firstSolider.Damage);
                firstSolider.TryUseSpecialAbility();
                secondSolider.TryUseSpecialAbility();

                platoonCountryRed.RemoveSoldier(firstSolider);
                platoonCountryBlue.RemoveSoldier(secondSolider);

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowBattleResult(Platoon platoonCountryRed, Platoon platoonCountryBlue)
        {
            if (platoonCountryRed.Count < 0 && platoonCountryBlue.Count < 0)
            {
                Console.WriteLine("Ничья, оба взвода погибли");
            }
            else if (platoonCountryRed.Count <= 0)
            {
                Console.WriteLine("Победила синяя страна");
            }
            else if (platoonCountryBlue.Count <= 0)
            {
                Console.WriteLine("Победила красная страна");
            }
        }
    }

    class PlatoonCreater
    {
        private static Random _random = new Random();

        public Platoon Create(int countSoldiers)
        {
            List<Soldier> army = new List<Soldier>();

            for (int i = 0; i < countSoldiers; i++)
            {
                List<Soldier> soldiers = new List<Soldier>();

                soldiers.Add(new Chemist("Химик", 100, 70, 20));
                soldiers.Add(new Air("Пилот", 160, 70, 40));
                soldiers.Add(new Infantry("Пехота", 200, 40, 15));
                soldiers.Add(new Sniper("Снайпер", 150, 60, 20));
                soldiers.Add(new Artillery("Альтилерия", 100, 50, 10));

                army.Add(soldiers[_random.Next(soldiers.Count)]);
            }

            return new Platoon(army);
        }
    }

    class Platoon
    {
        private List<Soldier> _soldiers = new List<Soldier>();

        public Platoon(List<Soldier> soldiers)
        {
            _soldiers = soldiers;
        }

        public int Count => _soldiers.Count;

        public void ShowInfo()
        {
            foreach (var soldier in _soldiers)
            {
                soldier.ShowStats();
            }

            Console.WriteLine();
        }

        public void RemoveSoldier(Soldier soldier)
        {
            if (soldier.Health <= 0)
            {
                _soldiers.Remove(soldier);
            }
        }

        public Soldier GetSoldier()
        {
            Random random = new Random();

            return _soldiers[random.Next(Count)];
        }
    }

    class Soldier
    {
        public Soldier(string name, int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public string Name { get;}
        public int Armor { get;}
        public int Health { get; protected set; }
        public int Damage { get; protected set; }

        public void Takedamage(int damage)
        {
            Health -= damage - Armor;
            Console.WriteLine($"\n{Name} нанёс {damage} урона.");
        }

        public void ShowStats()
        {
            Console.WriteLine($"{Name} - {Health} хп, {Damage} урона, {Armor} брони.");
        }

        public void TryUseSpecialAbility()
        {
            Random random = new Random();
            int randomNumber;
            int number = 1;
            int maximumNumber;
            int minimumNumber;
            maximumNumber = 4;
            minimumNumber = 1;
            randomNumber = random.Next(minimumNumber, maximumNumber);

            if (number == randomNumber)
            {
                Console.WriteLine();
                UseSpecialAbility();
            }
        }

        protected virtual void UseSpecialAbility() { }
    }

    class Artillery : Soldier
    {
        public Artillery(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseSpecialAbility()
        {
            Console.WriteLine($"{Name} начала попадать точно в цель");
            int plusDamage = 50;
            Damage += plusDamage;
        }
    }

    class Sniper : Soldier
    {
        public Sniper(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseSpecialAbility()
        {
            Console.WriteLine($"{Name} начал попадать в голову и наносить сокрушительный урон");
            int plusDamage = 100;
            Damage += plusDamage;
        }
    }

    class Infantry : Soldier
    {
        public Infantry(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseSpecialAbility()
        {
            Console.WriteLine($"{Name} начала использовать гранаты");
            int plusDamage = 40;
            Damage += plusDamage;
        }
    }

    class Air : Soldier
    {
        public Air(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseSpecialAbility()
        {
            Console.WriteLine($"{Name} начал бомбить ракетами");
            int plusDamage = 40;
            Damage += plusDamage;
        }
    }

    class Chemist : Soldier
    {
        public Chemist(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseSpecialAbility()
        {
            Console.WriteLine($"{Name} начал использовать ядовитый газ");
            int plusDamage = 2;
            Damage *= plusDamage;
        }
    }
}
