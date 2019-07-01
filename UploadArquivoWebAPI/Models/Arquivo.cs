using System;
using System.Collections.Generic;
using System.Text;

namespace UploadArquivoWebApi.Models
{
    public class Arquivo
    {
        public Arquivo(string nome, string tamanho, string dataCriacao, string hash)
        {
            this.Nome = nome;
            this.Tamanho = tamanho;
            this.DataCriacao = dataCriacao;
            this.Hash = hash;
        }

        public int Id { get; private set; }

        public string Nome { get; private set; }

        public string Tamanho { get; private set; }

        public string DataCriacao { get; private set; }

        public string Hash { get; set; }

    }
}
