﻿namespace WordNET_Server_2._0.DTOs
{
    public class AssociatedWordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Count { get; set; }

        public int StatisticsId { get; set; }
        public StatisticsDTO Statistics { get; set; } = null!;

        public int WordId { get; set; }
    }
}
