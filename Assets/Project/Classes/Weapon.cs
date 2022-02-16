using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Classes {
    public class Weapon {
        public enum ShotResult {
            NoAmmoInBackpack,
            NoAmmoInMagazine,
            ShotSuccessful,
            TooFast,
        }

        public const int DefaultShotDistance = 100;
        private int _bulletsPerShot;
        private int _amountOfBullets;
        private int _maxBulletsInMagazine;
        private int _currentAmmo;
        private bool _isReloading;
        private float _reloadTime;
        private float _delayBetweenShots;
        private DateTime _lastShotTime = DateTime.MinValue;
        private float _spread;
        public float MaxShotDistance { get; private set; }

        public event Action OnShot;
        public event Action OnReloadStart;
        public event Action OnReloadEnd;

        public Weapon(int bulletsPerShot, int amountOfBullets, int maxBulletsInMagazine, float reloadTime, float delayBetweenShots, float spread, float maxShotDistance) {
            _bulletsPerShot = bulletsPerShot;
            _amountOfBullets = amountOfBullets;
            _maxBulletsInMagazine = maxBulletsInMagazine;
            _reloadTime = reloadTime;
            _delayBetweenShots = delayBetweenShots;
            _spread = spread;
            MaxShotDistance = maxShotDistance;
            Reload();
        }

        public async Task ReloadTask() {
            if (_currentAmmo == _maxBulletsInMagazine)
            {
                return;
            }
            if (_amountOfBullets - _currentAmmo == 0)
            {
                return;
            }
            
            _isReloading = true;
            OnReloadStart?.Invoke();
            await Task.Delay((int)_reloadTime * 1000);
            Reload();
            OnReloadEnd?.Invoke();
            _isReloading = false;
        }

        private void Reload() {
            _currentAmmo = _amountOfBullets >= _maxBulletsInMagazine
                ? _maxBulletsInMagazine
                : _amountOfBullets % _maxBulletsInMagazine;
        }

        public ShotResult TryShoot(Vector2 muzzleHolePos, Quaternion muzzleHoleRot, out List<Ray2D> rays) {
            rays = new List<Ray2D>();

            if (_amountOfBullets == 0) {
                return ShotResult.NoAmmoInBackpack;
            }

            if ((DateTime.Now - _lastShotTime).TotalSeconds < _delayBetweenShots) {
                return ShotResult.TooFast;
            }

            if (_currentAmmo == 0) {
                if (_isReloading) return ShotResult.NoAmmoInMagazine;
                ReloadTask();
                return ShotResult.NoAmmoInMagazine;
            }

            _currentAmmo--;
            _amountOfBullets--;
            _lastShotTime = DateTime.Now;

            for (var i = 0; i < _bulletsPerShot; i++) {
                var localShootDir = muzzleHoleRot * SimpsonsSpreading.Spreading(_spread);
                rays.Add(new Ray2D(muzzleHolePos, localShootDir));
            }

            OnShot?.Invoke();
            if (_currentAmmo == 0) {
                ReloadTask();
            }
            return ShotResult.ShotSuccessful;
        }

        
    }
    public static class SimpsonsSpreading
    {
        private static float FindG(float random, float a)
        {
            return Mathf.Sqrt(2 * random) * a - a;
        }
        private static float FindRandNumberUsingSimpson(float random, float a)
        {
            return random <= 0.5 ? FindG(random, a) : -FindG(1 - random, a);
        }
        public static Vector2 Spreading(float a)
        {
            return new Vector2(FindRandNumberUsingSimpson(Random.value, a), Weapon.DefaultShotDistance).normalized;
        }
    }
}