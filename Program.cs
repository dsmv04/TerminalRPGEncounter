using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Crear enemigos
        Enemy zombie1 = new Zombie("Zombie 1");
        Enemy zombie2 = new Zombie("Zombie 2");
        Enemy spider = new Spider("Spider");

        // Crear héroes
        Ally ninja = new Ninja("Ninja");
        Ally samurai = new Samurai("Samurai");
        Ally wizard = new Wizard("Wizard");

        // Crear arrays para enemigos y aliados
        Enemy[] enemies = { zombie1, zombie2, spider };
        Ally[] allies = { ninja, samurai, wizard };

        // Iniciar el juego
        Console.WriteLine("¡Bienvenido a la batalla!");
        Console.WriteLine("Comienza el encuentro:");

        int currentPlayerIndex = 0;
        int currentEnemyIndex = 0;

        while (true)
        {
            // Verificar si el juego ha terminado
            if (AllAlliesDead(allies))
            {
                Console.WriteLine("¡Los enemigos ganaron!");
                break;
            }
            else if (AllEnemiesDead(enemies))
            {
                Console.WriteLine("¡Los aliados ganaron!");
                break;
            }

            // Imprimir estado actual
            Console.WriteLine("\nEstado actual:");
            Console.WriteLine($"Aliados: {string.Join(", ", allies.Select(a => a.ToString()).ToArray())}");
            Console.WriteLine($"Enemigos: {string.Join(", ", enemies.Select(e => e.ToString()).ToArray())}");

            // Turno actual
            Console.WriteLine($"\nTurno de {allies[currentPlayerIndex].Name}");
            Console.WriteLine("Selecciona un ataque (1 para ataque básico, 2 para especial):");
            string input = Console.ReadLine();

            if (input == "1")
            {
                allies[currentPlayerIndex].Attack(enemies[currentEnemyIndex]);
            }
            else if (input == "2")
            {
                allies[currentPlayerIndex].SpecialAttack(enemies[currentEnemyIndex]);
            }
            else
            {
                Console.WriteLine("Entrada no válida. Pierdes tu turno.");
            }

            // Pasar al siguiente jugador/enemigo
            currentPlayerIndex = (currentPlayerIndex + 1) % allies.Length;
            currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Length;
        }
    }

    static bool AllAlliesDead(Ally[] allies)
    {
        return allies.All(ally => ally.IsDead);
    }

    static bool AllEnemiesDead(Enemy[] enemies)
    {
        return enemies.All(enemy => enemy.IsDead);
    }
}

// Clase base para personajes
class Character
{
    public string Name { get; }

    public int Health { get; set; }
    public bool IsDead { get { return Health <= 0; } }

    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public override string ToString()
    {
        return $"{Name} ({Health} HP)";
    }
}

// Clase para enemigos
class Enemy : Character
{
    public Enemy(string name, int health = 50) : base(name, health) { }
}

// Clase para aliados
class Ally : Character
{
    public Ally(string name, int health = 100) : base(name, health) { }

    public virtual void Attack(Enemy enemy)
    {
        Console.WriteLine($"{Name} ataca a {enemy.Name}.");
        enemy.Health -= 10;
        Console.WriteLine($"{enemy.Name} tiene {enemy.Health} HP restantes.");
    }

    public virtual void SpecialAttack(Enemy enemy)
    {
        Console.WriteLine($"{Name} utiliza un ataque especial contra {enemy.Name}.");
        enemy.Health -= 20;
        Console.WriteLine($"{enemy.Name} tiene {enemy.Health} HP restantes.");
    }
}

// Clases específicas para aliados
class Ninja : Ally
{
    public Ninja(string name) : base(name) { }

    public override void SpecialAttack(Enemy enemy)
    {
        Console.WriteLine($"{Name} realiza un ataque ninja especial contra {enemy.Name}.");
        enemy.Health -= 30;
        Console.WriteLine($"{enemy.Name} tiene {enemy.Health} HP restantes.");
    }
}

class Samurai : Ally
{
    public Samurai(string name) : base(name) { }

    public override void SpecialAttack(Enemy enemy)
    {
        Console.WriteLine($"{Name} ejecuta un ataque samurái especial contra {enemy.Name}.");
        enemy.Health -= 25;
        Console.WriteLine($"{enemy.Name} tiene {enemy.Health} HP restantes.");
    }
}

class Wizard : Ally
{
    public Wizard(string name) : base(name) { }

    public override void SpecialAttack(Enemy enemy)
    {
        Console.WriteLine($"{Name} lanza un hechizo mágico contra {enemy.Name}.");
        enemy.Health -= 35;
        Console.WriteLine($"{enemy.Name} tiene {enemy.Health} HP restantes.");
    }
}

// Clases específicas para enemigos
class Zombie : Enemy
{
    public Zombie(string name) : base(name) { }
}

class Spider : Enemy
{
    public Spider(string name) : base(name) { }
}
