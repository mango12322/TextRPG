using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Models;

namespace TextRPG.Models
{
    internal class Player :Character
    {                
        public JobType Job { get; private set; }        
        public int Gold { get; private set; }
        public Equipment EquipedWeapon { get; private set; }
        public Equipment EquipedArmor { get; private set; }        

        
        public Player(string name, JobType job) : base(
            name: name,
            maxHp: GetInitHp(job),
            maxMp: GetInitMp(job),
            attackPower: GetInitAttack(job),
            defense: GetInitDefense(job),
            level: 1)
        {
            Job = job;
            Gold = 1000;
        }

        /* 초기 스탯 설정 메서드 */
        private static int GetInitHp(JobType job)
        {
            switch (job)
            {
                case JobType.Warrial: return 150;
                case JobType.Archer: return 100;
                case JobType.Wizard: return 80;
                default: return 100;
            }
        }

        private static int GetInitMp(JobType job)
        {
            switch (job)
            {
                case JobType.Warrial: return 30;
                case JobType.Archer: return 50;
                case JobType.Wizard: return 100;
                default: return 30;
            }
        }

        private static int GetInitAttack(JobType job) =>
            job switch
            {
                JobType.Warrial => 10,
                JobType.Archer => 15,
                JobType.Wizard => 20,
                _ => 20,
            };

        private static int GetInitDefense(JobType job)
        {
            switch (job)
            {
                case JobType.Warrial: return 15;
                case JobType.Archer: return 10;
                case JobType.Wizard: return 5;
                default: return 10;
            }

        }

        /* 캐릭터 정보 출력 메서드 재정의 */
        public override void PrintInfo()
        {
            // base.PrintInfo();
            Console.Clear();
            Console.WriteLine($"======= {Name} 정보 ======");
            Console.WriteLine($"레벌: {Level}");
            Console.WriteLine($"직업: {Job}");
            Console.WriteLine($"체력: {CurrentHp}/{MaxHp}");
            Console.WriteLine($"마나: {CurrentMp}/{MaxMp}");

            int attackBonus = EquipedWeapon != null ? EquipedWeapon.AttackBonus : 0;
            int defenseBonus = EquipedArmor != null ? EquipedArmor.DefenseBonus : 0;

            Console.WriteLine($"ATK: {AttackPower + attackBonus} (+{attackBonus})");
            Console.WriteLine($"DEF: {Defense + defenseBonus} (+{defenseBonus})");
            Console.WriteLine($"Gold: {Gold}");

            /* 장착 아이템 목록 */
            if (EquipedWeapon != null || EquipedArmor != null)
            {
                Console.WriteLine("\n[장착 중인 장비 목록");
                if (EquipedWeapon != null)
                {
                    Console.WriteLine($"- 무기: {EquipedWeapon.Name} (ATK +{EquipedWeapon.AttackBonus})");
                }

                if (EquipedArmor != null)
                {
                    Console.WriteLine($"- 방어구: {EquipedArmor.Name} (DEF +{EquipedArmor.DefenseBonus})");
                }
            }
            
        }

        /* 공격 메서드 재정의 */
        public override int Attack(Character target)
        {            
            int attackDamage = AttackPower;

            attackDamage += EquipedWeapon != null ? EquipedWeapon.AttackBonus : 0;

            return target.TakeDamage(attackDamage);
        }

        /* 스킬 공격 메서드 */
        public int SkillAttack(Character target)
        {
            int mpCost = 15;
            
            int totalDamage = AttackPower;
            totalDamage += EquipedWeapon != null ? EquipedWeapon.AttackBonus : 0;
            totalDamage = (int)(totalDamage * 1.5);
            
            CurrentMp -= mpCost;
            
            return target.TakeDamage(totalDamage);            
        }
        
        public void GainGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"\n{amount} 골드를 획득했습니다! 현재 골드: {Gold}");
        }

        /* 장비 장착 메서드 */
        public void EquipItem(Equipment newEquipment)
        {
            Equipment? prevEquipment = null;

            switch (newEquipment.Slot)
            {
                case EquipmentSlot.Weapon:
                    prevEquipment = EquipedWeapon;
                    EquipedWeapon = newEquipment;
                    break;

                case EquipmentSlot.Armor:
                    prevEquipment = EquipedArmor;
                    EquipedArmor = newEquipment;
                    break;

                default:
                    Console.WriteLine("잘못된 장비 슬롯입니다.");
                    return;
            }

            if (prevEquipment != null)
            {
                Console.WriteLine($"{prevEquipment.Name} 장착 해제");
            }

            Console.WriteLine($"{newEquipment.Name} 장착 완료");
        }

        /* 장비 해제 메서드 */
        public Equipment? UnequipItem(EquipmentSlot slot)
        {
            Equipment? equipment = null;

            switch (slot)
            {
                case EquipmentSlot.Weapon:
                    equipment = EquipedWeapon;
                    EquipedWeapon = null;
                    break;
                case EquipmentSlot.Armor:
                    equipment = EquipedArmor;
                    EquipedArmor = null;
                    break;
                default:
                    Console.WriteLine("잘못된 장비 슬롯입니다.");
                    return null;
            }

            if (equipment != null)
            {
                Console.WriteLine($"{equipment.Name} 장비 해제 완료");
            }
            else
            {
                Console.WriteLine("해제할 장비가 없습니다.");
            }

            return equipment;
        }
    }
}
