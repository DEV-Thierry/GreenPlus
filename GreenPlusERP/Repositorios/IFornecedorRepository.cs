﻿using GreenPlusERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenPlusERP.Repositorios
{
    public interface IFornecedorRepository
    {
        bool VerificaCNPJ(string cnpj);
        void Add(FornecedorModel fornecedor);
        void Edit(FornecedorModel fornecedor);
        void Remove(string cnpj);
        FornecedorModel GetByCnpj(string cnpj);
        FornecedorModel GetByNome(string nome);
    }
}