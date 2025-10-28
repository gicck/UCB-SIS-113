# Reusabilidad y Extensibilidad

## Principio
**Reusabilidad**: El código puede ser usado en múltiples contextos sin modificación
**Extensibilidad**: Se puede agregar nueva funcionalidad sin modificar el código existente (Principio Abierto/Cerrado)

## Ejemplo Malo (BadReport.cs)
- Lógica codificada directamente para casos de uso específicos
- Debe modificarse el código existente para agregar nuevos formatos
- Difícil de reutilizar componentes por separado
- Muchas cadenas de if/else

## Ejemplo Bueno (GoodReport/)
- Arquitectura de plugins para fácil extensión
- Componentes reutilizables
- Agregar nuevos formatos sin tocar el código existente
- Patrón Strategy para flexibilidad

## Demostración Rápida
```bash
dotnet run --project BadReport
dotnet run --project GoodReport
```

## Patrón Strategy
El patrón Strategy permite definir una familia de algoritmos, encapsular cada uno, y hacerlos intercambiables. Strategy permite que el algoritmo varíe independientemente de los clientes que lo usan.

### Beneficios Clave:
- **Open/Closed Principle**: Abierto para extensión, cerrado para modificación
- **Composition over Inheritance**: Composición de comportamiento en lugar de herencia
- **Single Responsibility**: Cada estrategia tiene una única responsabilidad
- **Testability**: Cada estrategia se puede probar independientemente
