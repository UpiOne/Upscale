using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
    
    static ServiceLocator()
    {
        Debug.LogWarning("ServiceLocator: СТАТИЧЕСКИЙ КОНСТРУКТОР ВЫЗВАН. Словарь сервисов был создан.");
    }

    public static void Register<T>(T service)
    {
        var type = typeof(T);
        if (service == null)
        {
            Debug.LogError($"ServiceLocator: Попытка зарегистрировать ПУСТОЙ (null) сервис типа {type.Name}!");
            return;
        }

        if (services.ContainsKey(type))
        {
            Debug.LogError($"ServiceLocator: Сервис типа {type.Name} уже зарегистрирован.");
            services[type] = service; 
            return;
        }
        services.Add(type, service);
        Debug.Log($"ServiceLocator: Сервис {type.Name} успешно зарегистрирован. Всего сервисов: {services.Count}");
    }

    public static T Get<T>()
    {
        var type = typeof(T);
        Debug.Log($"ServiceLocator: Поступил запрос на получение сервиса {type.Name}. Всего сервисов сейчас: {services.Count}");
        if (!services.TryGetValue(type, out var service))
        {
            Debug.LogError($"ServiceLocator: НЕ УДАЛОСЬ НАЙТИ сервис типа {type.Name}.");
            if (services.Count == 0)
            {
                Debug.LogError("ServiceLocator: Словарь сервисов ПУСТ!");
            }
            else
            {
                Debug.Log("ServiceLocator: Список доступных сервисов:");
                foreach (var availableService in services)
                {
                    Debug.Log($" - {availableService.Key.Name}");
                }
            }
            return default(T);
        }
        Debug.Log($"ServiceLocator: Сервис {type.Name} успешно найден и возвращен.");
        return (T)service;
    }

    public static void Clear()
    {
        Debug.LogWarning("ServiceLocator: ВЫЗВАН МЕТОД CLEAR. Все сервисы удалены!");
        services.Clear();
    }
}