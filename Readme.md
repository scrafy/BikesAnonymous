# BikesAnonymous

![Alt text!](logo.png)

### [1. Proyect description](#desc)

### [2. Layers description](#lay_desc)

### [3. How to start the application](#start)

### [4. Invoking methods using swagger UI](#invoke)

### [5. Criteria of acceptance](#acceptance)

1.  Cyclist authenticates]
2.  Cyclist prints his cyclist´s license]
3.  Owner loads cyclist data into DB]
4.  Owner authenticates](#own_auth)
5.  Owner gets last cyclists registered report by email](#report)
6.  Owner creates his own account](#create)<br><br><br>

### <a name="desc"></a>

### 1. Proyect description

<br>

<p>Bike Anonymous is an integral management platform which allows an administrator / owner of the same to carry out the management of federated cyclists.<br>
Through the platform, the administrator / owner will be able to perform various actions such as creating their own account, registering federated cyclists, obtaining a report via email of the registered cyclists on the last night, etc ... <br>
This platform will also allow federated cyclists to download their license on the fly so that in case they need it urgently, they can have it.
In short, it is a comprehensive management platform.</p>
<br>
<br>

### <a name="lay_desc"></a>

### 2. Layers description<br><br>

- Domain/core<br>

  <p>This layer represents our domain, our main entities.<br>
  Here we name our business, for example, what is a cyclist, what is an owner, what properties should they have, etc ...<br>
  Normally we will find an entity for each table in our database and they will have the same validation rules as the fields in the database.<br>
  Defining this layer correctly is very important since we will build our application on the data model that we define here.<br>
  All data that comes from outside our system and enters it, if it is necessary to store it, will have to be converted to a model of our domain and pass the validation rules that we have defined, otherwise, said information must be rejected.<br>
  With this we avoid contaminating our database with spurious data and incidentally avoid errors in our application.</p>
  <br>

- DataLayer/DataAcces<br>
  <p>This layer represents data access.<br>
  Defining this layer is a correct idea in the sense that it abstracts us from how we obtain the data in our application.<br>
  For example, if we used an ORM as an entity framework, we could define a new project with all the configuration of the entity framework and its entities and through this layer use all the power of the entity,
  or conversely, you could decide to use an object-oriented database like MongoDB.<br>
  For this example we have used plain text json files as the data storage system, but if we wanted to change it, we would simply have to reprogram only this layer, not affecting the rest of the application layers.<br>
  The important idea or concept to understand here is that we abstract ourselves into a data access layer, we can rest assured if in the future we want to change the data persistence system of our application since we will only have to work on this layer.<br>
  It is an essential condition that this layer works only with the models of our domain, that is, all the methods of ALL the interfaces that are programmed in this layer must receive models of our domain for both reading and writing operations.</p>
