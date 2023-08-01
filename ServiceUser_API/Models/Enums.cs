namespace ServiceUser_API.Models
{
    public enum UserRole{
        Student,
        Teacher,
        Admin
    }
    public enum Status{
        Active,
        Inactive
    }
    public enum Relationship
    {
        Padre,
        Madre,
        Tutor,
        Otro
    }
    public enum Provinces
    {
        // Provincias del Ecuador
        Azuay,
        Bolivar,
        Canar,
        Carchi,
        Chimborazo,
        Cotopaxi,
        ElOro,
        Esmeraldas,
        Galapagos,
        Guayas,
        Imbabura,
        Loja,
        LosRios,
        Manabi,
        MoronaSantiago,
        Napo,
        Orellana,
        Pastaza,
        Pichincha,
        SantaElena,
        SantoDomingoDeLosTsachilas,
        Sucumbios,
        Tungurahua,
        ZamoraChinchipe
    }

    public enum Cities
    {
        // Ciudades del Ecuador
        Ambato, Atacames, Azogues, Babahoyo, BahiaDeCaraquez, Balzar, Cayambe, Chone, Cuenca, Daule, Duran, ElCarmen, Esmeraldas, Guaranda, Guayaquil, Huaquillas, Ibarra, Jipijapa, LaLibertad, LaMana, LaTroncal, Latacunga, Loja, Machachi, Machala, Manta, Milagro, Montecristi, Naranjal, Naranjito, Otavalo, Pasaje, Pedernales, Playas, Portoviejo, Puyo, Quevedo, Quito, Riobamba, RosaZarate, Salinas, SantaElena, SantaRosa, SantoDomingo, Tulcan, Ventanas, Yaguachi
    }
}