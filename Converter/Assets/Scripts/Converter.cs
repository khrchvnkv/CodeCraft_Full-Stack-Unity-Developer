using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace Homework
{
    /**
       Конвертер представляет собой преобразователь ресурсов, который берет ресурсы
       из зоны погрузки (справа) и через несколько секунд преобразовывает его в
       ресурсы другого типа (слева).
       
       Конвертер работает автоматически. Когда заканчивается цикл переработки
       ресурсов, то конвертер берет следующую партию и начинает цикл по новой, пока
       можно брать ресурсы из зоны загрузки или пока есть место для ресурсов выгрузки.
       
       Также конвертер можно выключать. Если конвертер во время работы был
       выключен, то он возвращает обратно ресурсы в зону загрузки. Если в это время
       были добавлены еще ресурсы, то при переполнении возвращаемые ресурсы
       «сгорают».
     
       Характеристики конвертера:
       - Зона погрузки: вместимость бревен
       - Зона выгрузки: вместимость досок
       - Кол-во ресурсов, которое берется с зоны погрузки
       - Кол-во ресурсов, которое поставляется в зону выгрузки
       - Время преобразования ресурсов
       - Состояние: вкл/выкл
     */
    public sealed class Converter
    {
        //TODO: Написать конвертер ресурсов по TDD
        private readonly int _loadingCapacity;
        private readonly int _unloadingCapacity;
        private readonly int _fromResourcesAmount;
        private readonly int _toResourcesAmount;
        private readonly float _conversionTime;
        
        private float _timer;
        private bool _isEnabled;
        
        public bool Converting { get; private set; }
        public int LoadingValue { get; private set; }
        public int UnloadingValue { get; private set; }
        public bool IsEnabled
        {
            get => _isEnabled;
            private set
            {
                _isEnabled = value;
                if (_isEnabled)
                {
                    StartConvert();
                }
                else
                {
                    if (Converting)
                    {
                        AddLoadingValue(_fromResourcesAmount);
                        Converting = false;
                    }
                }
            }
        }

        private float Timer
        {
            get => _timer;
            set
            {
                _timer = Math.Max(0, value);
                if (_timer <= 0)
                {
                    if (Converting)
                    {
                        CompleteConvert();
                    }
                    else
                    {
                        TryStartConverter();   
                    }
                }
            }
        }

        public event Action OnConverted;

        public Converter(
            in int loadingCapacity,
            in int unloadingCapacity,
            in int fromResourcesAmount,
            in int toResourcesAmount,
            in float conversionTime)
        {
            if (loadingCapacity <= 0) throw new ArgumentOutOfRangeException($"{nameof(loadingCapacity)} must be greater zero");
            if (unloadingCapacity <= 0) throw new ArgumentOutOfRangeException($"{nameof(unloadingCapacity)} must be greater zero");
            if (fromResourcesAmount <= 0) throw new ArgumentOutOfRangeException($"{nameof(fromResourcesAmount)} must be greater zero");
            if (toResourcesAmount <= 0) throw new ArgumentOutOfRangeException($"{nameof(toResourcesAmount)} must be greater zero");
            if (conversionTime <= 0.0f) throw new ArgumentOutOfRangeException($"{nameof(conversionTime)} must be greater zero");
            
            _loadingCapacity = loadingCapacity;
            _unloadingCapacity = unloadingCapacity;
            _fromResourcesAmount = fromResourcesAmount;
            _toResourcesAmount = toResourcesAmount;
            _conversionTime = conversionTime;

            LoadingValue = 0;
            UnloadingValue = 0;
        }

        public void Update(float deltaTime)
        {
            if (IsEnabled)
            {
                Timer -= deltaTime;
            }
        }

        public void Enable() => IsEnabled = true;

        public void Disable() => IsEnabled = false;

        public void AddLoadingValue(in int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException();
            
            LoadingValue = Math.Min(_loadingCapacity, LoadingValue + value);
        }
        
        internal void AddUnloadingValue(in int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException();

            UnloadingValue = Math.Min(_unloadingCapacity, UnloadingValue + value);
        }

        public void RemoveUnloadingValue(in int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException();

            UnloadingValue -= value;
            TryStartConverter();
        }

        private void TryStartConverter()
        {
            if (!Converting && CanStartConverter())
            {
                StartConvert();
            }
        }

        internal bool CanStartConverter() =>
            LoadingValue >= _fromResourcesAmount &&
            UnloadingValue + _toResourcesAmount <= _unloadingCapacity;

        private void StartConvert()
        {
            if (CanStartConverter())
            {
                LoadingValue -= _fromResourcesAmount;
                Timer = _conversionTime;
                Converting = true;
            }
        }

        private void CompleteConvert()
        {
            UnloadingValue = Math.Min(_unloadingCapacity, UnloadingValue + _toResourcesAmount);
            Converting = false;
            OnConverted?.Invoke();
        }
    }
}