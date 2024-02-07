# E-commerce Project



## Usecases
USECASE: 1/Guest
Bruger starter på homepage. Han går ind på Products Page, og vælger et produkt til kurven.
Han går til checkout for at indtaste sine oplysninger, og bestiller sin ordre. Hvorefter han bliver
sendt hen til ordre konfirmations siden hvor han kan se sin ordre.

USECASE: 2/Register
Bruger starter på homepage, Bruger vil gerne lave en profil, og går til registrerings siden.
Han udfylder formen, og bliver sendt over til login hvor han skal logge ind med sin nye bruger.
Her efter går han ind på Products Page, og vælger et produkt til kurven.
Han går til checkout for at indtaste sine oplysninger, og bestiller sin ordre. Hvorefter han bliver
sendt hen til ordre konfirmations siden hvor han kan se sin ordre.

USECASE: 3/Variant
Bruger starter på homepage. Han går ind på Products Page, og vælger et produkt, han vil gerne have en
anden variant af produktet, og tilføjer derfor en variant til sin kurv.
Han går til checkout for at indtaste sine oplysninger, og bestille sin ordre. Hvorefter han bliver
sendt hen til ordre konfirmations siden hvor han kan se sin ordre.

USECASE: 4/Quantity
Bruger starter på homepage. Han går ind på Products Page, og vælger et produkt, hvorefter han vælger at
sætte antal til 2 og tilføjer dem til sin kurv.
Han fortryder senere og ændre antallet i kurven til 1.
Han går til checkout for at indtaste sine oplysninger, og bestiller sin ordre. Hvorefter han bliver
sendt hen til ordre konfirmations siden hvor han kan se sin ordre.

USECASE: 5/Admin
Admin starter på homepage, og går til Admin login ved at tilgå admin URL'en og logger ind med hans admin
login.
Herefter klikker han ind på produktControl for at lave et nyt produkt. han vælger derefter at tilføje
en produktvariant i produktVariantControl. Derefter går han ud på siden for at teste om produktet er på
produktpage.

## Requirements
For at køre programmet skal der tilføjes en Connection string og en SALT
```
"ConnectionStrings": {
    "Connection": "CONNECTION_STRING"
  },
  "SALT": "RANDOM_SALT"
```
For at køre testen skal man tilføje en Secret.json E-commerce.Test\Create data for local database\Secret.json og hvis man ønsker at tilføje fakedata via at køre FillDatabaseWithData.cs skal man også tilføje Connection string ellers skal man bare tilføje SALT:
```
"ConnectionStrings": {
    "Connection": "CONNECTION_STRING"
  },
  "SALT": "RANDOM_SALT"
```

I VScode skal man adde en Environment fil med følgende i
```
export const environment = {
    API_URL: "https://localhost:7094/api/",
};
```


## Tests

Vores test bruger Collection fra Xunit som gør at før testen fra en fil/class kører så bliver der lavet dependency injection så man får skabt en fake databasecontext til hver test fil/class.

CreateFakeDBDependenciesInjector.cs -- Definere hvad der skal injects og i vores tilfælde er det CreateFakeDBDependencies.cs

I CreateFakeDBDependencies Som åbner en SQLite connection i memory til hver test fil.

hvorefter den skaber en fake databasecontext som man så kan bruge i testene og når de er kørt færdig vil den blive disposed.
