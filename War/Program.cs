using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Platoon _platoonCountryRed = new Platoon();
        private Platoon _platoonCountryBlue = new Platoon();
        private Soldier _firstSolider;
        private Soldier _secondSolider;

        public void OpenMenu()
        {
            Console.WriteLine("Война между взводами двух стран, краной и синей");
            Console.ReadKey();
            Battle();
            ShowBattleResult();
        }

        public void Battle()
        {
            while (_platoonCountryRed.GetCountSolders() > 0 && _platoonCountryBlue.GetCountSolders() > 0)
            {
                _firstSolider = _platoonCountryRed.GetSoldierFromPlatoon();
                _secondSolider = _platoonCountryBlue.GetSoldierFromPlatoon();
                Console.WriteLine("Красный взвод:");
                _platoonCountryRed.ShowPlatoon();
                Console.WriteLine("Синий взвод:");
                _platoonCountryBlue.ShowPlatoon();
                _firstSolider.Takedamage(_secondSolider.Damage);
                _secondSolider.Takedamage(_firstSolider.Damage);
                _firstSolider.UseAnAttack();
                _secondSolider.UseAnAttack();
                RemoveSoldier();
                Console.ReadKey();
                //System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }

        private void ShowBattleResult()
        {
            if (_platoonCountryRed.GetCountSolders() < 0 && _platoonCountryBlue.GetCountSolders() < 0)
            {
                Console.WriteLine("Ничья, оба взвода погибли");
            }
            else if (_platoonCountryRed.GetCountSolders() <= 0)
            {
                Console.WriteLine("Победила синяя страна");
            }
            else if (_platoonCountryBlue.GetCountSolders() <= 0)
            {
                Console.WriteLine("Победила красная страна");
            }
        }

        private void RemoveSoldier()
        {
            if (_firstSolider.Health < 0)
            {
                _platoonCountryRed.RemoveSoldierFromplatoon(_firstSolider);
            }
            else if (_secondSolider.Health < 0)
            {
                _platoonCountryBlue.RemoveSoldierFromplatoon(_secondSolider);
            }
        }
    }

    class Platoon
    {
        private List<Soldier> _soldiers = new List<Soldier>();
        private Soldier soldier;
        private Random _random = new Random();

        public Platoon()
        {
            CreateNewPlatoon(10, _soldiers);
        }

        public Soldier GetSoldierFromPlatoon()
        {
            return _soldiers[_random.Next(0, _soldiers.Count)];
        }

        public void ShowPlatoon()
        {
            foreach (var soldier in _soldiers)
            {
                soldier.ShowStats();
            }
            Console.WriteLine();
        }

        public void RemoveSoldierFromplatoon(Soldier soldier)
        {
            _soldiers.Remove(soldier);
        }

        public void ShowSoldiers()
        {
            for (int i = 0; i < _soldiers.Count; i++)
            {
                Console.Write(i + 1 + "- ");
                _soldiers[i].ShowStats();
            }
        }

        public int GetCountSolders()
        {
            return _soldiers.Count;
        }

        private void CreateNewPlatoon(int numberOfPlatoon, List<Soldier> soldier)
        {
            for (int i = 0; i < numberOfPlatoon; i++)
            {
                soldier.Add(GetSoldier());
            }
        }

        private Soldier GetSoldier()
        {
            Random random = new Random();
            int randomSoldierClass;
            int maximumSoldierClass;
            int minimumSoldierClass;
            maximumSoldierClass = 5;
            minimumSoldierClass = 0;
            randomSoldierClass = random.Next(minimumSoldierClass, maximumSoldierClass);

            if (randomSoldierClass == 0)
            {
                return new Artillery("Альтилерия", 100, 50, 10);
            }
            else if (randomSoldierClass == 1)
            {
                return new Sniper("Снайпер", 150, 60, 20);
            }
            else if (randomSoldierClass == 2)
            {
                return new Infantry("Пехота", 200, 40, 15);
            }
            else if (randomSoldierClass == 3)
            {
                return new Air("Пилот", 160, 70, 40);
            }
            else
            {
                return new Chemist("Химик", 100, 70, 20);
            }
        }
    }

    class Soldier
    {
        public string Name { get; private set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }

        public Soldier(string name, int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public void Takedamage(int damage)
        {
            Health -= damage - Armor;
            Console.WriteLine($"\n{Name} нанёс {damage} урона.");
        }

        public void ShowStats()
        {
            Console.WriteLine($"{Name} - {Health} хп, {Damage} урона, {Armor} брони.");
        }

        public void UseAnAttack()
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
                UseAttack();
            }
        }

        protected virtual void UseAttack() { }
    }

    class Artillery : Soldier
    {
        public Artillery(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseAttack()
        {
            Console.WriteLine($"{Name} начала попадать точно в цель");
            int plusDamage = 50;
            Damage += plusDamage;
        }
    }

    class Sniper : Soldier
    {
        public Sniper(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseAttack()
        {
            Console.WriteLine($"{Name} начал попадать в голову и наносить сокрушительный урон");
            int plusDamage = 100;
            Damage += plusDamage;
        }
    }

    class Infantry : Soldier
    {
        public Infantry(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseAttack()
        {
            Console.WriteLine($"{Name} начала использовать гранаты");
            int plusDamage = 40;
            Damage += plusDamage;
        }
    }

    class Air : Soldier
    {
        public Air(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseAttack()
        {
            Console.WriteLine($"{Name} начал бомбить ракетами");
            int plusDamage = 40;
            Damage += plusDamage;
        }
    }

    class Chemist : Soldier
    {
        public Chemist(string name, int health, int damage, int armor) : base(name, health, damage, armor) { }

        protected override void UseAttack()
        {
            Console.WriteLine($"{Name} начал использовать ядовитый газ");
            int plusDamage = 2;
            Damage *= plusDamage;
        }
    }
}
