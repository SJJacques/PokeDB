USE PokemonDB;

INSERT INTO Types (TypeName) VALUES
('Fire'), ('Water'), ('Grass'), ('Electric'), ('Normal');

INSERT INTO Abilities (AbilityName) VALUES
('Overgrow'), ('Blaze'), ('Torrent'), ('Static'), ('Run Away');

INSERT INTO Moves (MoveName, AttackPower, Accuracy, TypeId) VALUES
('Tackle', 40, 100, (SELECT Id FROM Types WHERE TypeName = 'Normal')),
('Thunder Shock', 40, 100, (SELECT Id FROM Types WHERE TypeName = 'Electric')),
('Ember', 40, 100, (SELECT Id FROM Types WHERE TypeName = 'Fire')),
('Water Gun', 40, 100, (SELECT Id FROM Types WHERE TypeName = 'Water')),
('Vine Whip', 45, 100, (SELECT Id FROM Types WHERE TypeName = 'Grass'));

INSERT INTO Pokemon (PokemonName, BaseHp, BaseAttack, BaseDefense, TypeId, AbilityId) VALUES
('Pikachu', 35, 55, 40,
    (SELECT Id FROM Types WHERE TypeName = 'Electric'),
    (SELECT Id FROM Abilities WHERE AbilityName = 'Static')),
('Charmander', 39, 52, 43,
    (SELECT Id FROM Types WHERE TypeName = 'Fire'),
    (SELECT Id FROM Abilities WHERE AbilityName = 'Blaze')),
('Squirtle', 44, 48, 65,
    (SELECT Id FROM Types WHERE TypeName = 'Water'),
    (SELECT Id FROM Abilities WHERE AbilityName = 'Torrent')),
('Bulbasaur', 45, 49, 49,
    (SELECT Id FROM Types WHERE TypeName = 'Grass'),
    (SELECT Id FROM Abilities WHERE AbilityName = 'Overgrow'));

INSERT INTO PokemonMoves (PokemonId, MoveId)
VALUES
-- Pikachu: Tackle + Thunder Shock
((SELECT Id FROM Pokemon WHERE PokemonName = 'Pikachu'), (SELECT Id FROM Moves WHERE MoveName = 'Tackle')),
((SELECT Id FROM Pokemon WHERE PokemonName = 'Pikachu'), (SELECT Id FROM Moves WHERE MoveName = 'Thunder Shock')),

-- Charmander: Tackle + Ember
((SELECT Id FROM Pokemon WHERE PokemonName = 'Charmander'), (SELECT Id FROM Moves WHERE MoveName = 'Tackle')),
((SELECT Id FROM Pokemon WHERE PokemonName = 'Charmander'), (SELECT Id FROM Moves WHERE MoveName = 'Ember')),

-- Squirtle: Tackle + Water Gun
((SELECT Id FROM Pokemon WHERE PokemonName = 'Squirtle'), (SELECT Id FROM Moves WHERE MoveName = 'Tackle')),
((SELECT Id FROM Pokemon WHERE PokemonName = 'Squirtle'), (SELECT Id FROM Moves WHERE MoveName = 'Water Gun')),

-- Bulbasaur: Tackle + Vine Whip
((SELECT Id FROM Pokemon WHERE PokemonName = 'Bulbasaur'), (SELECT Id FROM Moves WHERE MoveName = 'Tackle')),
((SELECT Id FROM Pokemon WHERE PokemonName = 'Bulbasaur'), (SELECT Id FROM Moves WHERE MoveName = 'Vine Whip'));
