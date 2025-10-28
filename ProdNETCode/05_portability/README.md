# Portabilidad

## Principio
**Portabilidad**: El código debe funcionar en diferentes plataformas, entornos y configuraciones sin modificación.

Aspectos clave:
- Usar bibliotecas multiplataforma
- Evitar rutas codificadas directamente
- Abstraer detalles específicos del entorno
- Usar archivos de configuración

## Ejemplo Malo (BadFileHandler.cs)
- Rutas de Windows codificadas directamente
- Código específico de plataforma
- Suposiciones de entorno incrustadas en el código
- No funcionará en Linux/Mac

## Ejemplo Bueno (GoodFileHandler.cs)
- Manejo de rutas multiplataforma
- Abstracción del entorno
- Basado en configuración
- Funciona en cualquier sistema operativo

## Demostración Rápida
```bash
dotnet run --project BadFileHandler
dotnet run --project GoodFileHandler
```

## Configuración
Ver `appsettings.json` y `appsettings.Development.json` para ejemplos de configuración por entorno.

### Beneficios de la Portabilidad:
- **Cross-platform**: Funciona en Windows, Linux, macOS sin cambios
- **Environment-agnostic**: Fácil de desplegar en dev/staging/production
- **Configuration-driven**: Cambios sin recompilar
- **Testability**: Fácil de probar con diferentes configuraciones
