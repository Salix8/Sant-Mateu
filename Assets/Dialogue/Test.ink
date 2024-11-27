INCLUDE globals.ink

{armaElegida =="": -> main | -> already_chose}

=== main ===

Has sido elegido para luchar en nombre del clan

Elige un arma y parte hacia tu destino
    + [Espada y escudo]
        -> chosen("Espada y escudo")
    + [Mandoble]
        -> chosen("Mandoble")
    + [Arco y flechas]
        -> chosen("Arco y flechas")

=== chosen(weapon) ===
~ armaElegida = weapon
Has elegido {weapon}!
-> END

=== already_chose ===
Ya has elegido un arma {armaElegida}.
-> END