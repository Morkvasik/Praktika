
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Visilitsa_2._0
{
    public partial class MainPage : ContentPage
    {
        // Слова для игры
        string[] slova =
        {
            "КОМПЬЮТЕР",
            "ПИТОН",
            "ПРОГРАММА",
            "МОНИТОР",
            "ТЕЛЕФОН",
            "ИНТЕРНЕТ",
            "АЛГОРИТМ",
            "КЛАВИАТУРА"
        };

        // Вопросы к словам
        string[] voprosy =
        {
            "Как называется устройство для работы с программами?",
            "Как называется язык программирования?",
            "Что пишут программисты?",
            "На чём отображается изображение?",
            "Как называется устройство для звонков?",
            "Что объединяет множество компьютеров по всему миру?",
            "Как называется порядок действий для решения задачи?",
            "На чём печатают текст на компьютере?"
        };

        string slovo = "";
        HashSet<char> bukvi = new HashSet<char>();

        int oshibki = 0;
        int maxOshibok = 6;

        Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
            NovayaIgra();
        }

        // Начинаем новую игру
        void NovayaIgra()
        {
            int nomer = random.Next(slova.Length);

            slovo = slova[nomer];
            Vopros.Text = voprosy[nomer];

            // Очищаем старые буквы и ошибки
            bukvi.Clear();
            oshibki = 0;

            // Включаем все кнопки и возвращаем им синий цвет
            foreach (View element in PoleBukv.Children)
            {
                if (element is Button knopka)
                {
                    knopka.IsEnabled = true;
                    knopka.BackgroundColor = Color.FromArgb("#315D9B");
                    knopka.TextColor = Colors.White;
                }
            }

            Obnovit();
        }

        // Действие при нажатии на букву
        private void Bukva_Click(object? sender, EventArgs e)
        {
            if (sender is not Button knopka)
                return;

            char bukva = knopka.Text[0];

            // Не даём повторно использовать букву
            if (bukvi.Contains(bukva))
                return;

            bukvi.Add(bukva);

            // Проверяем, есть ли буква в слове
            if (slovo.Contains(bukva))
            {
                // Правильная буква становится зелёной
                knopka.BackgroundColor = Color.FromArgb("#287D53");
            }
            else
            {
                // Неправильная буква становится красной
                oshibki++;
                knopka.BackgroundColor = Colors.Red;
            }

            // Отключаем нажатую кнопку
            knopka.IsEnabled = false;

            Obnovit();
        }

        // Обновляем слово, рисунок и сообщение
        void Obnovit()
        {
            // Показываем рисунок по количеству ошибок
            Risunok.Source = ImageSource.FromFile(
                "viselitsa" + oshibki + ".png"
            );

            OshibkiText.Text = "Ошибки: " + oshibki + " из " + maxOshibok;

            // Показываем угаданные буквы и пропуски
            string slovoNaEkrane = "";

            foreach (char bukva in slovo)
            {
                if (bukvi.Contains(bukva))
                    slovoNaEkrane += bukva + " ";
                else
                    slovoNaEkrane += "_ ";
            }

            SlovoText.Text = slovoNaEkrane.Trim();

            // Проверяем, угадано ли всё слово
            bool pobeda = true;

            foreach (char bukva in slovo)
            {
                if (!bukvi.Contains(bukva))
                {
                    pobeda = false;
                    break;
                }
            }

            // Если игрок победил
            if (pobeda)
            {
                Soobshenie.Text = "Победа! Ты угадал слово!";
                Soobshenie.TextColor = Color.FromArgb("#287D53");
            }
            // Если закончились попытки
            else if (oshibki >= maxOshibok)
            {
                Soobshenie.Text = "Ты проиграл! Слово: " + slovo;
                Soobshenie.TextColor = Colors.Red;

                // Показываем правильное слово
                SlovoText.Text = string.Join(" ", slovo.ToCharArray());
            }
            else
            {
                Soobshenie.Text = "Выбирай букву!";
                Soobshenie.TextColor = Color.FromArgb("#287D53");
            }

            // После победы или проигрыша отключаем все буквы
            if (pobeda || oshibki >= maxOshibok)
            {
                foreach (View element in PoleBukv.Children)
                {
                    element.IsEnabled = false;
                }
            }
        }

        // Кнопка начала новой игры
        private void NewGame_Click(object? sender, EventArgs e)
        {
            NovayaIgra();
        }
    }
}
