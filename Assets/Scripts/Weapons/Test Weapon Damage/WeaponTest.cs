using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDamageTests
{
    private GameObject playerObject;
    private Player player;
    private GameObject sliderObject;
    private Slider slider;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject("PlayerDummy");
        sliderObject = new GameObject("SliderDummy");

        slider = sliderObject.AddComponent<Slider>();
        player = playerObject.AddComponent<Player>();
        player.slider = slider;
        player.CurrentHealth = 100;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(sliderObject);
    }

    [Test]
    public void Player_TakesDamageFromLeekWeapon_HealthReducesCorrectly()
    {
        int initialHealth = player.CurrentHealth;
        int damageLeek = 7;

        player.TakeDamage(damageLeek);

        Assert.AreEqual(initialHealth - damageLeek, player.CurrentHealth);
        Assert.AreEqual(93, player.CurrentHealth);
    }

    [Test]
    public void IDamageable_MockTarget_ReceivesDamageProperly()
    {
        var dummyEnemy = new DummyTarget(50);

        dummyEnemy.TakeDamage(7);

        Assert.AreEqual(43, dummyEnemy.Health);
    }

    private class DummyTarget : IDamageable
    {
        public int Health { get; private set; }
        public DummyTarget(int health) => Health = health;
        public void TakeDamage(int damageAmount) => Health -= damageAmount;
    }
}