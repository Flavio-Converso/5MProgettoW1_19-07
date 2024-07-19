﻿using Project.Models;

namespace Project.Services
{
    public interface IViolazioneService
    {
        Violazione Create(Violazione violazione);  
        List<Violazione> GetAllViolazioni(); 
        List<ViolazioneOver10Punti> GetViolazioneOver10Punti(); 
    }
}
