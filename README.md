# Guía Técnica para Instalar y Probar el Proyecto SeveClie

## 1. Instalación y Configuración Local del Proyecto

### 1.1 Requisitos Previos

- **.NET 9.0 SDK**: Asegúrate de tener instalado el SDK de .NET 9.0.
- **SQL Server**: Asegúrate de tener acceso a una instancia de SQL Server para crear la base de datos.
- **Postman**: Para realizar pruebas de los servicios API.

### 1.2 Pasos para Descargar e Instalar el Proyecto

1. **Clonar el repositorio del proyecto**: Si no lo has hecho aún, clona el repositorio desde el control de versiones con el siguiente comando:

    ```bash
    git clone https://github.com/AndrewBabativa/SeveClie.git
    cd SeveClie
    ```

2. **Restaurar las dependencias**: Asegúrate de que todas las dependencias necesarias estén instaladas:

    ```bash
    dotnet restore
    ```

3. **Configurar la base de datos**: El proyecto usa **Entity Framework Core** para manejar migraciones y acceso a la base de datos. Asegúrate de tener configurado un archivo `appsettings.json` con la cadena de conexión correcta.

4. **Aplicar migraciones**: Ejecuta el siguiente comando para aplicar las migraciones y crear la base de datos:

    ```bash
    dotnet ef database update
    ```

5. **Ejecutar el proyecto localmente**: Para iniciar el proyecto en el entorno local, ejecuta:

    ```bash
    dotnet run
    ```

    El servidor debería estar corriendo en `http://localhost:5000` o el puerto configurado.

## 2. Autenticación con JWT

El proyecto utiliza **JWT** (JSON Web Tokens) para autenticar a los usuarios y proteger los servicios de la API. A continuación se describe cómo obtener el token de autenticación.

### 2.1 Servicio de Login

- **URL**: `POST /api/auth/login`
- **Cuerpo de la solicitud**:

    ```json
    {
      "user": "test",
      "password": "password"
    }
    ```

Este servicio devolverá un token JWT que se utilizará en las siguientes solicitudes como `Authorization: Bearer {authToken}`.

### 2.2 Controlador de Autenticación

El controlador que maneja la autenticación es **AuthController**, y se encuentra bajo el namespace `SeveClie.Application.Controllers`. La acción `Login` recibe el nombre de usuario y la contraseña en el cuerpo de la solicitud y devuelve un token que se debe usar para autenticar futuras solicitudes a la API.

### 2.3 Uso del Token en las Solicitudes

Una vez obtenido el token de autenticación, debes incluirlo en los encabezados de todas las solicitudes que requieran autenticación. Para hacer esto en Postman, hemos configurado una variable global `Bearer {{authToken}}`, donde `authToken` será el valor devuelto por el servicio `Login`.

## 3. Servicios Expuestos

A continuación se detallan los servicios expuestos por la API, los cuales requieren autenticación mediante el token JWT.

### 3.1 Crear un Cliente

- **Método**: `POST`
- **URL**: `https://localhost:44384/api/Clie`
- **Cuerpo (Body)**:

    ```json
    {
      "Cedula": "1234567890",
      "Nombre": "Andres Babativa Goyeneche",
      "Genero": "Masculino",
      "FechaNac": "1991-09-28",
      "EstadoCivil": "Soltero"
    }
    ```

- **Requiere Token**: Sí

### 3.2 Actualizar un Cliente

- **Método**: `PUT`
- **URL**: `https://localhost:44384/api/Clie/1`
- **Cuerpo (Body)**:

    ```json
    {
      "Cedula": "1234567890",
      "Nombre": "Juan Pérez Actualizado",
      "Genero": "Masculino",
      "FechaNac": "1985-10-15",
      "EstadoCivil": "Casado"
    }
    ```

- **Requiere Token**: Sí

### 3.3 Obtener Todos los Clientes

- **Método**: `GET`
- **URL**: `https://localhost:44384/api/Clie`
- **Requiere Token**: Sí

### 3.4 Obtener un Cliente por ID

- **Método**: `GET`
- **URL**: `https://localhost:44384/api/Clie/1`
- **Requiere Token**: Sí

### 3.5 Eliminar un Cliente

- **Método**: `DELETE`
- **URL**: `https://localhost:44384/api/Clie/1`
- **Requiere Token**: Sí

## 4. Modelo de Datos: ClieDto

Este es el modelo utilizado para crear y editar clientes. A continuación, se describen las validaciones de los campos del modelo para asegurar que los datos cumplan con las restricciones definidas.

- **Cedula**:
    - **Requerido**: Sí
    - **Descripción**: La cédula es obligatoria y debe ser un valor único, no puede superar los 20 caracteres.
- **Nombre**:
    - **Requerido**: Sí
    - **Descripción**: El nombre del cliente es obligatorio y no puede exceder los 100 caracteres.
- **Genero**:
    - **Requerido**: Sí
    - **Descripción**: El género del cliente debe especificarse y no puede exceder los 10 caracteres.
- **FechaNac**:
    - **Requerido**: Sí
    - **Descripción**: La fecha de nacimiento es obligatoria y debe ser una fecha válida en formato de tipo DateTime.
- **EstadoCivil**:
    - **Requerido**: Sí
    - **Descripción**: El estado civil del cliente es obligatorio y no puede exceder los 50 caracteres.

## 5. Manual de Usuario para Pruebas en Postman

Este documento te guiará sobre cómo realizar pruebas en los servicios de la API utilizando Postman.

### 5.2 Ejecutar la solución localmente, se abrira Swagger mostrando los servicios disponibles y tenemos como referencia el puerto local donde se esta ejecutando la aplicación para mas adelante configurar los servicios en Postman si es necesario:

![image](https://github.com/user-attachments/assets/211d933f-46d0-42bd-835c-27f952575d71)

### 5.3 Importar la Colección de Postman

1. **Descargar la colección**: Descarga el archivo `SeveClie.postman_collection.json` desde el directorio `docs/postman` del repositorio.
2. **Importar en Postman**:
    - Abre Postman.
    - Haz clic en el botón **Import**.
    - Selecciona el archivo descargado y cárgalo en Postman.

### 5.4 Obtener el Token de Seguridad

1. Selecciona la solicitud `Login` de la colección. Configura el cuerpo de la solicitud como tipo **raw** con formato JSON:

    ```json
    {
      "user": "test",
      "password": "password"
    }
    ```

2. Haz clic en **Send**. El servicio devolverá un token de seguridad en el campo `authToken`. Este token se guarda automáticamente en la variable de entorno `authToken`.

![image](https://github.com/user-attachments/assets/5c8bfe47-2485-4cba-87e3-6a3226e2f2e4)


### 5.3 Configuración de la Variable de Entorno

1. **Verifica la variable**:
    - Ve al menú **Environments** en Postman.
    - Selecciona el entorno correspondiente (por ejemplo: `Local`).
    - Verifica que la variable `authToken` tenga un valor asignado.

2. **Usar el token en los servicios**: Todos los servicios de la API ahora usan esta variable automáticamente en el encabezado con la clave `Authorization: Bearer {{authToken}}`.

![image](https://github.com/user-attachments/assets/40b9c4ec-4f2d-4af4-8b35-bc575e55b469)

Aquí tienes la versión actualizada con los nombres correctos de los servicios:

---

### 5.4 Consumo de API `Clie`

#### 1. **Crear Cliente (POST /api/Clie)**

1. **Selecciona la solicitud `Crear Cliente`** de la colección.
2. **Configura el cuerpo de la solicitud** como tipo **raw** con formato JSON. Un ejemplo de cuerpo sería el siguiente:

    ```json
    {
      "cedula": "1234567890",
      "nombre": "Juan Pérez",
      "genero": "Masculino",
      "fechaNac": "1990-01-01",
      "estadoCivil": "Soltero"
    }
    ```

3. **Configura el encabezado**:
    - Ve a la pestaña **Headers** y agrega el encabezado:
      ```
      Authorization: Bearer {{authToken}}
      ```

4. **Haz clic en `Send`**.
    - Si la creación es exitosa, recibirás una respuesta con el estado 201 y los datos del cliente creado.

    **Respuesta esperada:**
    ```json
    {
      "message": "Cliente creado con éxito.",
      "clientId": "12345"
    }
    ```

![image](https://github.com/user-attachments/assets/252725af-2de2-4829-946a-b9cbfe872617)

#### 2. **Consultar Todos los Clientes (GET /api/Clie)**

1. **Selecciona la solicitud `Consultar Todos los Clientes`** de la colección.
2. **Configura la URL** para obtener todos los clientes:

    ```
    https://api.clie.com/api/Clie
    ```

3. **Configura el encabezado**:
    - En la pestaña **Headers**, agrega el encabezado:
      ```
      Authorization: Bearer {{authToken}}
      ```

4. **Haz clic en `Send`**.
    - La API devolverá una lista de todos los clientes.

    **Respuesta esperada:**
    ```json
    [
      {
        "cedula": "1234567890",
        "nombre": "Juan Pérez",
        "genero": "Masculino",
        "fechaNac": "1990-01-01",
        "estadoCivil": "Soltero"
      },
      {
        "cedula": "0987654321",
        "nombre": "Ana Gómez",
        "genero": "Femenino",
        "fechaNac": "1985-05-15",
        "estadoCivil": "Casada"
      }
    ]
    ```

![image](https://github.com/user-attachments/assets/dc2cbecb-c4ba-41c8-bd23-df81ab096e84)

#### 3. **Actualizar Cliente (PUT /api/Clie/{id})**

1. **Selecciona la solicitud `Actualizar Cliente`** de la colección.
2. **Configura la URL** con el `id` del cliente que deseas actualizar:

    ```
    https://api.clie.com/api/Clie/12345
    ```

3. **Configura el cuerpo de la solicitud** como tipo **raw** con formato JSON:

    ```json
    {
      "nombre": "Juan Pérez Actualizado",
      "genero": "Masculino",
      "fechaNac": "1990-01-01",
      "estadoCivil": "Casado"
    }
    ```

4. **Configura el encabezado**:
    - En la pestaña **Headers**, agrega el encabezado:
      ```
      Authorization: Bearer {{authToken}}
      ```

5. **Haz clic en `Send`**.
    - Si la actualización es exitosa, recibirás una respuesta con el estado 200 y los datos actualizados del cliente.

    **Respuesta esperada:**
    ```json
    {
      "message": "Cliente actualizado con éxito.",
      "clientId": "12345"
    }
    ```

![image](https://github.com/user-attachments/assets/8eb2b106-c578-42f4-b0d5-5a67f0b2d55f)

#### 4. **Eliminar Cliente (DELETE /api/Clie/{id})**

1. **Selecciona la solicitud `Eliminar Cliente`** de la colección.
2. **Configura la URL** con el `id` del cliente que deseas eliminar:

    ```
    https://api.clie.com/api/Clie/12345
    ```

3. **Configura el encabezado**:
    - En la pestaña **Headers**, agrega el encabezado:
      ```
      Authorization: Bearer {{authToken}}
      ```

4. **Haz clic en `Send`**.
    - Si la eliminación es exitosa, recibirás una respuesta con el estado 200.

    **Respuesta esperada:**
    ```json
    {
      "message": "Cliente eliminado con éxito."
    }
    ```

![image](https://github.com/user-attachments/assets/7c466b7d-9732-43f6-8435-8a93ec92846e)

---

## 6. Arquitectura de Aplicación: Patrones y Estructura

### Patrones de Diseño Implementados

#### **Unit of Work**

- **Objetivo**: El patrón **Unit of Work** mantiene un registro de las operaciones realizadas sobre las entidades del dominio, asegurando que todas las modificaciones a la base de datos sean tratadas de manera atómica.
- **Implementación**: Se utiliza dentro del repositorio para gestionar la persistencia de las entidades. Las operaciones sobre la entidad `ClieEntity` se manejan como un conjunto transaccional.

#### **Command and Query Responsibility Segregation (CQRS)**

- **Objetivo**: Separa las operaciones de lectura y escritura en el sistema para optimizar el rendimiento.
- **Implementación**:
    - **Comandos (Commands)**: Definen las intenciones de cambio en el sistema.
    - **Consultas (Queries)**: Se encargan de obtener datos sin modificar el estado.
    - Las operaciones sobre `ClieEntity` se gestionan mediante comandos como `CreateClieCommand`, `UpdateClieCommand`, y `DeleteClieCommand`.

#### **Repository Pattern**

- **Objetivo**: Encapsula el acceso a los datos y abstrae la lógica de acceso a la base de datos.
- **Implementación**: El repositorio `ClieRepository` proporciona métodos para agregar, actualizar, y eliminar clientes, lo que facilita la interacción con la base de datos mediante un enfoque orientado a objetos.

Aquí tienes el texto modificado con el formato de **Objetivo** e **Implementación** para el patrón **Factory**:

#### **Factory en `ClieEntity`**

- **Objetivo**:  
El patrón **Factory** centraliza la lógica de validación y creación de objetos complejos. En lugar de depender de constructores públicos, que no pueden realizar validaciones o aplicar reglas de negocio, el patrón **Factory** asegura que las instancias de la entidad `ClieEntity` se creen de manera controlada, garantizando que los datos sean válidos antes de su creación.

- **Implementación**:  
En este proyecto, se ha utilizado el patrón **Factory** para crear instancias de la entidad `ClieEntity`. La clase `ClieEntity` tiene un constructor privado para evitar su creación directa. En lugar de utilizar el constructor directamente, se implementa el método estático `Create`, que se encarga de realizar las validaciones necesarias antes de crear la instancia de la entidad. Si los datos no cumplen con las validaciones, el método lanza una excepción, asegurando que solo se creen entidades válidas.

```csharp
public static ClieEntity Create(string cedula, string nombre, string genero, DateTime fechaNac, string estadoCivil)
{
    if (string.IsNullOrWhiteSpace(cedula)) throw new ArgumentException("La cédula no puede estar vacía.", nameof(cedula));
    if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
    if (string.IsNullOrWhiteSpace(genero)) throw new ArgumentException("El género no puede estar vacío.", nameof(genero));
    if (string.IsNullOrWhiteSpace(estadoCivil)) throw new ArgumentException("El estado civil no puede estar vacío.", nameof(estadoCivil));

    return new ClieEntity
    {
        Cedula = cedula,
        Nombre = nombre,
        Genero = genero,
        FechaNac = fechaNac,
        EstadoCivil = estadoCivil
    };
}
```

---


