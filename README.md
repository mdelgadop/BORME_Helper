# BORME Asistente
Se ha creado una aplicación mínima para la gestión de fechas en las que el usuario ha revisado el BORME.
# Interfaz de usuario
La interfaz tiene 3 partes:
 - Calendario: el usuario elegirá una fecha en la que descargar el BORME
 - Provincia: una vez elegida la fecha, la aplicación cargará las provincias disponibles. El usuario elegirá una provincia.
 - Descarga: una vez elegida la fecha y la provincia, el usuario pulsará el botón de descarga para abrir el BORME (la aplicación abrirá el navegador automáticamente) y marcar el día como ya leído (en verde).
# Diseño
Aunque haya poco código, se ha elegido un diseño por capas:
 - Capa Screen: basado en MVVM, tiene la interfaz de usuario y su lógica en un ViewModel.
 - Capa Application: contiene el servicio con la lógica de validación. Utiliza el patrón Request-Response para que el ViewModel se comunique con el Service, para que ante cualqier futuro cambio, la interfaz no se modifique.
 - Capa Business: contiene el repositorio donde se guardan los días marcados. Contiene también un mock del repositorio para hacer los tests unitarios y el DBContext para el acceso a base de datos SQLite.
 - Capa Core: la idea era guardar aquí módulos comunes: patrones, enumerados, excepciones... Sólo ha sido necesario guardar un patrón, pero con el diseño esta capa podría haber sido prescindible.
 - Capa Test: contiene los tests unitarios realizados. 
# Decisiones
 - Se ha utilizado el patrón Factory para la instanciación del servicio y del repositorio, por simplicidad. Para una aplicación más grande sería mejor haber usado un framework de inyección de dependencias, como Unity.
 - Para MVVM se ha asignado directamente el DataContext en el Code Behind, igualmente por simplicidad. Para una aplicación más grande sería mejor haber usado un framework como Caliburn Micro o similar.
 - Sólo hay dos entidades: Provincia y Fecha, en una relación N..M. Sólo se ha creado la tabla de relación, por lo que, otra vez por simplicidad, no se han creado entidades ad hoc, lo que hibiera significado hacer sus correspondientes Dtos y los mappers.
 - La entidad Provincia podría haber sido un enumerado, pero dado que no hay una cantidad fija, sino que depende del día, se ha creído conveniente guardarlo como cadena de caracteres.
 - Debido igualmente a la simplicidad, se ha utilizado el motor SQLite, con el que se puede hacer una base de datos muy simple y portable en un único fichero. No se considera para algo en producción, ya que con el paso del tiempo se guardarían muchos registros, además de posibles ampliaciones; es sólo para un MVP.
 - Tampoco he usado un framework como Entity Framework, algo que sí usaría para una aplicación más grande o en producción, eliminando las sentencias SQL puestas en el repositorio.
