﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzz.Models
{
    [Serializable]
    public class AnswerViewModel
    {
        public bool IsCorrect { get; set; }
        public string AnswerString { get; set; }
    }
}