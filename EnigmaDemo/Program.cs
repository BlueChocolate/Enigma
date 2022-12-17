// See https://aka.ms/new-console-template for more information
using EnigmaCore;

Console.WriteLine("Hello, World!");
Enigma enigma= new Enigma();
enigma.Rotors.Add(new Rotor());
enigma.Rotors.Add(Rotor.RotorLibrary.RotorII);
enigma.Rotors.Add(Rotor.RotorLibrary.RotorVIII);
enigma.Reflector = Reflector.ReflectorLibrary.ReflectorB;
enigma.Plugboard=Plugboard.PlugboardLibrary.PlugboardCross;
Console.WriteLine(Enigma.GetJsonByEnigma(enigma));
