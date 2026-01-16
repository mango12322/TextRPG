using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG.Models
{
    /* 모든 캐릭터의 공통 속성과 메서드를 정의하는 추상 클래스 */
    internal abstract class Character
    {
        public string Name { get; protected set; }
        public int CurrentHp { get; protected set; }
        public int MaxHp { get; protected set; }
        public int CurrentMp { get; protected set; }
        public int MaxMp { get; protected set; }
        public int AttackPower { get; protected set; }
        public int Defense { get; protected set; }
        public int Level { get; protected set; }

        public bool IsAlive => CurrentHp > 0;

        public Character(string name, int maxHp, int maxMp, int attackPower, int defense, int level)
        {
            Name = name;
            MaxHp = maxHp;
            CurrentHp = maxHp;
            MaxMp = maxMp;
            CurrentMp = maxMp;
            AttackPower = attackPower;
            Defense = defense;
            Level = level;
        }

        // 공통으로 사용할 메서드들
        // 추상 메서드(abstract method) : 자식 클래스에서 반드시 구현해야 하는 메서드
        public abstract int Attack(Character target);

        // 데미지 처리 메소드
        // 가상 메서드(virtual method) : 자식 클래스에서 필요에 따라 재정의할 수 있는 메서드
        public virtual int TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            CurrentHp = Math.Max(0, CurrentHp - actualDamage);

            return actualDamage;
        }

        /* 캐릭터 정보 출력 메서드 */
        public virtual void PrintInfo()
        {
            Console.Clear();
            Console.WriteLine($"\n====== {Name}님 캐릭터 정보 ======");
            Console.WriteLine($"레벨: {Level}");
            Console.WriteLine($"체력: {CurrentHp}/{MaxHp}");
            Console.WriteLine($"마나: {CurrentMp}/{MaxMp}");
            Console.WriteLine($"공격력: {AttackPower}");
            Console.WriteLine($"방어력: {Defense}");
        }

        public int HealHp(int amount)
        {
            int beforeHp = CurrentHp;
            CurrentHp = Math.Min(MaxHp, CurrentHp + amount);
            return CurrentHp - beforeHp;
        }

        public int HealMp(int amount)
        {
            int beforeMp = CurrentMp;
            CurrentMp = Math.Min(MaxMp, CurrentMp + amount);
            return CurrentMp - beforeMp;
        }
    }
}
