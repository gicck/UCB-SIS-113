# Mantenibilidad y Testeabilidad

## Principio
**Mantenibilidad**: El código debe ser fácil de entender, modificar y depurar
**Testeabilidad**: El código debe ser fácil de probar en aislamiento

Aspectos clave:
- Estructura y nomenclatura clara
- Funciones puras cuando sea posible
- Inyección de dependencias para pruebas
- Separación de responsabilidades

## Ejemplo Malo (BadCalculator.cs)
- Lógica compleja y anidada
- Efectos secundarios mezclados con cálculos
- Dependencias codificadas directamente
- Difícil de probar
- Sin pruebas incluidas

## Ejemplo Bueno (GoodCalculator/)
- Funciones claras y simples
- Funciones puras
- Fácil de probar
- Suite de pruebas completa incluida
- Bien documentado

## Demostración Rápida
```bash
dotnet run --project BadCalculator
dotnet run --project GoodCalculator
dotnet test GoodCalculator.Tests
```

## Beneficios de la Testeabilidad:
- **Pure Functions**: Sin efectos secundarios, siempre mismo resultado para misma entrada
- **Dependency Injection**: Permite reemplazar dependencias con mocks en pruebas
- **Single Responsibility**: Cada componente hace una cosa, fácil de probar
- **Separation of Concerns**: Lógica de negocio separada de I/O y efectos secundarios
