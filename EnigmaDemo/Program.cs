// See https://aka.ms/new-console-template for more information
using EnigmaCore;

Console.WriteLine("Hello, World!");
Enigma enigma= new Enigma();
enigma.Rotors.Add(Rotor.RotorLibrary.RealRotorI);
enigma.Rotors.Add(Rotor.RotorLibrary.RealRotorII);
enigma.Rotors.Add(Rotor.RotorLibrary.RealRotorVIII);

Console.WriteLine(Enigma.GetJsonByEnigma(enigma));
