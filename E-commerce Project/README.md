# E-commerce Project



## Usecases
USECASE: 1/Guest
Bruger starter p� homepage. Han g�r ind p� Products Page, og v�lger et produkt til kurven.
Han g�r til checkout for at indtaste sine oplysninger, og bestiller sin ordre. Hvorefter han bliver
sendt hen til ordre konfirmations siden hvor han kan se sin ordre.

USECASE: 2/Register
Bruger starter p� homepage, Bruger vil gerne lave en profil, og g�r til registrerings siden.
Han udfylder formen, og bliver sendt over til login hvor han skal logge ind med sin nye bruger.
Her efter g�r han ind p� Products Page, og v�lger et produkt til kurven.
Han g�r til checkout for at indtaste sine oplysninger, og bestiller sin ordre. Hvorefter han bliver
sendt hen til ordre konfirmations siden hvor han kan se sin ordre.

USECASE: 3/Variant
Bruger starter p� homepage. Han g�r ind p� Products Page, og v�lger et produkt, han vil gerne have en
anden variant af produktet, og tilf�jer derfor en variant til sin kurv.
Han g�r til checkout for at indtaste sine oplysninger, og bestille sin ordre. Hvorefter han bliver
sendt hen til ordre konfirmations siden hvor han kan se sin ordre.

USECASE: 4/Quantity
Bruger starter p� homepage. Han g�r ind p� Products Page, og v�lger et produkt, hvorefter han v�lger at
s�tte antal til 2 og tilf�jer dem til sin kurv.
Han fortryder senere og �ndre antallet i kurven til 1.
Han g�r til checkout for at indtaste sine oplysninger, og bestiller sin ordre. Hvorefter han bliver
sendt hen til ordre konfirmations siden hvor han kan se sin ordre.

USECASE: 5/Admin
Admin starter p� homepage, og g�r til Admin login ved at tilg� admin URL'en og logger ind med hans admin
login.
Herefter klikker han ind p� produktControl for at lave et nyt produkt. han v�lger derefter at tilf�je
en produktvariant i produktVariantControl. Derefter g�r han ud p� siden for at teste om produktet er p�
produktpage.

## Requirements
For at k�re programmet skal der tilf�jes en Connection string og en SALT
```
"ConnectionStrings": {
    "Connection": "CONNECTION_STRING"
  },
  "SALT": "RANDOM_SALT"
```
For at k�re testen skal man tilf�je en Secret.json E-commerce.Test\Create data for local database\Secret.json og adde:
```
"ConnectionStrings": {
    "Connection": "CONNECTION_STRING"
  },
  "SALT": "RANDOM_SALT"
```

I VScode skal man adde en Environment fil med f�lgende i
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
