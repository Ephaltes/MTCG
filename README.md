# MTCG
## Technischer Ablauf

Die HttpVerbindung baut auf dem Webserver (vorige Abgabe) auf.

Für ankommende Anfragen gibt es jeweilige Handler, die sich um den jeweiligen Endpoint kümmern
mit deren Verbs (POST;DELETE;GET;PUT)

Die Handler führen einfache Validierungen durch und leiten den Code sonst weiter an die jeweiligen Modelle,
wo die Logik liegt.

Sollte das Modell Daten aus der Datenbank brauchen wird dort die Verbindung zur Datenbank aufgebaut und die 
Jeweilige Anfrage abgesetzt.

Die Datenbank Klasse kümmert sich um das Abrufen/Hinzufügen/Ändern der Daten und um die Validierung der 
Abgefragten Daten sowie um das Konvertieren in die jeweilige Entity.

Das Resultat wird vom Handler dann wieder an den Client gesendet.

## Unit Tests

Es gibt Unit test für jede Handler, Modell Klasse.
Die Unittests schauen ob sich die jeweiligen Klassen wie vorgestellt unter verschiedenen Bedingungen richtig verhalten.
Bei diesen Test wurde die Datenbank Klasse NICHT getestet, da diese nicht mockable ist und der Aufwand um
die Datenbank zu simulieren sehr hoch ist.


## Integration Tests

Da beim erstellen der Karten, eine UID erstellt wird, kann der Integrationtest leider nicht
vollständig automatisiert werden.

## Class Diagram 

Siehe Projektordner mit ClassDiagram

## Time Spent

Für das Projekt wurden ~40h aufgewendet
Es wurde am Anfang des Semester schon damit begonnen,
da sich aber im laufenden noch die Spezifikationen geändert
hatten musste ein wenig anpassungsarbeit durchgeführt werden.

## Submodule

git submodule update --init --recursive --remote


## GitHub

https://github.com/Ephaltes/MTCG

## Datenbank
Scripts für die Datenbank/Integrationstest befinden sich im /Scripts ordner