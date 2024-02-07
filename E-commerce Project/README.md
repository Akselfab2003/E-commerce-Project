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
For at køre testen skal man tilføje en Secret.json E-commerce.Test\Create data for local database\Secret.json og adde:
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


## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
