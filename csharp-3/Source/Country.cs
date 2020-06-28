using System;
using System.Collections.Generic;
using System.Linq;

namespace Codenation.Challenge
{
    public class Country
    {
        public State[] Top10StatesByArea()
        {
            return BrazilianStates().OrderByDescending(_ => _.Area).Take(10).ToArray();
        }

        public List<State> BrazilianStates()
        {
            List<State> BrazilianStates = new List<State>();

            BrazilianStates.Add(new State("Acre", "AC", 164123.040));
            BrazilianStates.Add(new State("Alagoas", "AL", 27778.506));
            BrazilianStates.Add(new State("Amapá", "AP", 142828.521));
            BrazilianStates.Add(new State("Amazonas", "AM", 1559159.148));
            BrazilianStates.Add(new State("Bahia", "BA", 564733.177));
            BrazilianStates.Add(new State("Ceará", "CE", 148920.472));
            BrazilianStates.Add(new State("Distrito Federal", "DF", 5779.999));
            BrazilianStates.Add(new State("Espirito Santo", "ES", 46095.583));
            BrazilianStates.Add(new State("Goiás", "GO", 340111.783));
            BrazilianStates.Add(new State("Maranhão", "MA", 331937.450));
            BrazilianStates.Add(new State("Mato Grosso", "MT", 903366.192));
            BrazilianStates.Add(new State("Mato Grosso do Sul", "MS", 357145.532));
            BrazilianStates.Add(new State("Minas Gerais", "MG", 586522.122));
            BrazilianStates.Add(new State("Pará", "PA", 1247954.666));
            BrazilianStates.Add(new State("Paraíba", "PB", 56585.000));
            BrazilianStates.Add(new State("Paraná", "PR", 199307.922));
            BrazilianStates.Add(new State("Pernambuco", "PE", 98311.616));
            BrazilianStates.Add(new State("Piauí", "PI", 251577.738));
            BrazilianStates.Add(new State("Rio de Janeiro", "RJ", 43780.172));
            BrazilianStates.Add(new State("Rio Grande do Norte", "RN", 52811.047));
            BrazilianStates.Add(new State("Rio Grande do Sul", "RS", 281730.223));
            BrazilianStates.Add(new State("Rondônia", "RO", 237590.547));
            BrazilianStates.Add(new State("Roraima", "RR", 224300.506));
            BrazilianStates.Add(new State("Santa Catarina", "SC", 95736.165));
            BrazilianStates.Add(new State("São Paulo", "SP", 248222.362));
            BrazilianStates.Add(new State("Sergipe", "SE", 21915.116));
            BrazilianStates.Add(new State("Tocantins", "TO", 277720.520));
            return BrazilianStates;
        }
    }
}
