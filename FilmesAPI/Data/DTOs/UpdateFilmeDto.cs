using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs
{
    public class UpdateFilmeDto
    {
        [StringLength(300, ErrorMessage = "O campo Título não deve ter mais de 300 caracteres")] //Essa anotação diz que o parâmetro deve ter no máximo x caracteres
        [Required(ErrorMessage = "O campo Título é obrigatório")] //Essa anotação diz que o parâmetro é de preenchimento obrigatório
        public string Titulo { get; set; }

        [StringLength(100, ErrorMessage = "O campo Diretor não deve ter mais de 100 caracteres")]
        [Required(ErrorMessage = "O campo Diretor é obrigatório")]
        public string Diretor { get; set; }

        [StringLength(30, ErrorMessage = "O campo Gênero não deve ter mais de 30 caracteres")]
        [Required(ErrorMessage = "O campo Gênero é obrigatório")]
        public string Genero { get; set; }


        [Range(1, 600, ErrorMessage = "A duração deve ser entre 1 e 600 minutos")] //Essa anotação indica que o valor do parâmetro deve estar entre x, y
        public int Duracao { get; set; }
    }
}
