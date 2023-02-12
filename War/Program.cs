using System;
using System.Collections.Generic;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            Battlefield battlefield = new Battlefield();
            battlefield.OpenMenu();
        }
    }

    class Battlefield
    {
        private Platoon _platoonCountryRed = new Platoon(15);
        private Platoon _platoonCountryBlue = new Platoon(15);

        public void OpenMenu()
        {
            Console.WriteLine("Война между взводами двух стран, краной и синей");
            Console.ReadKey();
            Battle();
            ShowBattleResult();
        }

        public void Battle()
        {
            while (_platoonCountryRed.Count > 0 && _platoonCountryBlue.Count > 0)
            {
                Soldier firstSolider = _platoonCountryRed.GetSoldier();
                Soldier secondSolider = _platoonCountryBlue.GetSoldier();

                Console.WriteLine("Красный взвод:");
                _platoonCountryRed.ShowPlatoon();
                Console.WriteLine("Синий взвод:");
                _platoonCountryBlue.ShowPlatoon();

                firstSolider.Takedamage(secondSolider.Damage);
                secondSolider.Takedamage(firstSolider.Damage);
                firstSolider.TryUseSpecialAbility();
                secondSolider.TryUseSpecialAbility();

                _platoonCountryRed.RemoveSoldier(firstSolider);
                _platoonCountryBlue.RemoveSoldier(secondSolider);

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowBattleResult()
        {
            if (_platoonCountryRed.Count < 0 && _platoonCountryBlue.Count < 0)
            {
                Console.WriteLine("Ничья, оба взвода погибли");
            }
            else if (_platoonCountryRed.Count <= 0)
            {
                Console.WriteLine("Победила синяя страна");
            }
            else if (_platoonCountryBlue.Count <= 0)
            {
                Console.WriteLine("Победила красная страна");
            }
        }
    }

    class Platoon
    {
        private List<Soldier> _soldiers = new List<Soldier>();

        public Platoon(int countSoldiers)
        {
            CreateNewPlatoon(countSoldiers);
        }

        public int Count => _soldiers.Count;

        public void ShowPlatoon()
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

        private void CreateNewPlatoon(int countSoldiers)
        {
            Random random= new Random();

            for (int i = 0; i < countSoldiers; i++)
            {
                List<Soldier> soldiers = new List<Soldier>();

                soldiers.Add(new Chemist("Химик", 100, 70, 20));
                soldiers.Add(new Air("Пилот", 160, 70, 40));
                soldiers.Add(new Infantry("Пехота", 200, 40, 15));
                soldiers.Add(new Sniper("Снайпер", 150, 60, 20));
                soldiers.Add(new Artillery("Альтилерия", 100, 50, 10));

                _soldiers.Add(soldiers[random.Next(soldiers.Count)]);
            }
            
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
