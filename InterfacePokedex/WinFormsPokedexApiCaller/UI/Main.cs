using Microsoft.Extensions.Options;
using PokedexApiCaller.Config;
using PokedexApiCaller.Contract.v1.In;
using PokedexApiCaller.Contract.v1.Out;
using PokedexApiCaller.Interfaces;
using PokedexApiCaller.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPokedexApiCaller.UI;

public partial class Main : Form
{
    private int _numberOfPages = 0;
    private int _currentPage = 0;
    private int _itensPerPage = 8;
    private readonly PokedexSettings _pokedexSettings;
    private readonly IPokemonApiCaller _pokemonApiCaller;
    private readonly IPokedexVersionApiCaller _pokedexApiCaller;
    private readonly IAuthApiCaller _authApiCaller;

    //public List<PokedexVersion> PokedexVersions { get; set; }

    public Main(IOptions<PokedexSettings> pokedexSettings, IPokemonApiCaller pokemonApiCaller, IAuthApiCaller authApiCaller, IPokedexVersionApiCaller pokedexApiCaller)
    {
        _pokedexSettings = pokedexSettings.Value;
        _pokemonApiCaller = pokemonApiCaller;
        _authApiCaller = authApiCaller;
        _pokedexApiCaller = pokedexApiCaller;
        InitializeComponent();
        Start();
    }

    #region Private Methods

    private void Start()
    {
        _currentPage = 1;
        AuthenticateApi();
        SetNumberOfPages();
        FillComboBoxes();
        ChangePage();
    }

    private void FillComboBoxes()
    {
        var versions = _pokedexApiCaller.GetVersion(0).Result;

        List<object> vers = new List<object>();
        List<object> searchVers = new List<object>();
        foreach (var version in versions)
        {
            vers.Add(new { Text = version.Name, Value = version.Id });
            searchVers.Add(new { Text = version.Name, Value = version.Id });
        }
        cbSearchVersion.DataSource = searchVers;
        cbVersion.DataSource = vers;
    }

    private void AuthenticateApi()
    {
        _pokemonApiCaller.Auth = _authApiCaller.GetToken("Joao").Result;
        _pokedexApiCaller.Auth = _authApiCaller.GetToken("Joao").Result;
    }

    private void ChangePage()
    {
        ClearMiniImages();
        GetPage();
        CheckNavigationButtons();
    }

    private void ClearMiniImages()
    {
        ClearPanel(pnlPokemon1);
        ClearPanel(pnlPokemon2);
        ClearPanel(pnlPokemon3);
        ClearPanel(pnlPokemon4);
        ClearPanel(pnlPokemon5);
        ClearPanel(pnlPokemon6);
        ClearPanel(pnlPokemon7);
        ClearPanel(pnlPokemon8);
    }

    private async Task GetPage()
    {
        var pokemons = await Task.Run(() => _pokemonApiCaller.GetPagedBasicInfo(_currentPage, _itensPerPage));
        LoadBasicInfo(pokemons);
    }

    private void LoadBasicInfo(OutGetBasicInfoPokemon pokemons)
    {
        for (int i = 0; i < pokemons.Count; i++)
        {
            var pnl = (Panel)pnlMiniPokemons.Controls.Find($"pnlPokemon{i + 1}", false).First();
            pnl.Tag = pokemons[i].Id;
            LoadImage(pnl, pokemons[i].ImageName);
        }
    }

    private void SetNumberOfPages()
    {
        int numberOfPokemons = (int)_pokemonApiCaller.GetNumberOfPokemons().Result;
        int number = numberOfPokemons / _itensPerPage;
        int roundNumber = numberOfPokemons % _itensPerPage;
        _numberOfPages = number + (roundNumber > 0 ? 1 : 0);
    }

    private void CheckNavigationButtons()
    {
        btnFirst.Enabled = _currentPage != 1;
        btnPrevious.Enabled = _currentPage != 1;
        btnNext.Enabled = _currentPage != _numberOfPages;
        btnLast.Enabled = _currentPage != _numberOfPages;
    }

    private void LoadImage(Panel pnl, string file)
    {
        var path = Path.Combine(_pokedexSettings.BaseImagePath, file);
        pnl.BackgroundImage = new Bitmap(path);
    }

    private void ClearPanel(Panel pnl)
    {
        pnl.BackgroundImage = null;
        pnl.Tag = null;
    }

    private async Task LoadPokemonInfo(int pokemonId, int versionId)
    {
        PokemonInfo pokemonInfo = await Task.Run(() => _pokemonApiCaller.GetInfo(pokemonId, versionId));
        if (pokemonInfo.Id > 0)
            LoadPokemonInfo(pokemonInfo);
        else
            txtDescription.Text = "Pokemon didn't exist in this version";
    }

    private void LoadPokemonInfo(PokemonInfo pokemon)
    {
        LoadImage(pnlImage, pokemon.ImageName);
        pnlImage.Tag = pokemon.Id;
        txtName.Text = pokemon.Name;
        cbVersion.SelectedValue = pokemon.Versions.Last().VersionId;
        txtDescription.Text = WriteDescription(pokemon, (int)cbVersion.SelectedValue);

        if (pokemon.EvolvesFrom != null)
        {
            pnlPreEvolutionImage.Tag = pokemon.EvolvesFrom.Id;
            LoadImage(pnlPreEvolutionImage, pokemon.EvolvesFrom.ImageName);
        }
        if (pokemon.EvolvesTo.Any())
            LoadEvolutionsPanel(pokemon.EvolvesTo);
    }

    private void LoadEvolutionsPanel(List<PokemonInfo> pokemons)
    {
        pnlAllEvolutions.Controls.Clear();
        int a = pnlAllEvolutions.Height / 49;
        int initialPosition = 2;
        if (a > pokemons.Count)
        {
            initialPosition = (pnlAllEvolutions.Height / 2) - (46 / 2);
            initialPosition -= (pokemons.Count - 1) * 25;
        }
        foreach (var pok in pokemons)
        {
            var pnl = CreatePanelEvolution(initialPosition, pok.Id);
            LoadImage(pnl, pok.ImageName);
            pnlAllEvolutions.Controls.Add(pnl);
            initialPosition += 50;
        }
    }

    private Panel CreatePanelEvolution(int top, int tag)
    {
        Panel pnl = new Panel();
        pnl.BackgroundImageLayout = ImageLayout.Stretch;
        pnl.Location = new Point(3, top);
        pnl.Name = "pnlEvolution";
        pnl.Size = new Size(46, 46);
        pnl.Tag = tag;
        pnl.Click += pnlPokemon_Click;
        return pnl;
    }

    private string WriteDescription(PokemonInfo pokemon, int version)
    {
        var descr = new StringBuilder();
        descr.AppendLine($"ID: {pokemon.Id}");
        descr.AppendLine("Description:");
        descr.AppendLine(pokemon.Versions.First(v => v.VersionId == version).Description);
        descr.AppendLine($"Height: {pokemon.Height}");
        descr.AppendLine($"Weight: {pokemon.Weight}");
        descr.AppendLine($"Generation: {pokemon.GenerationNumber}");
        descr.AppendLine(WriteTypes(pokemon.Types));
        return descr.ToString();
    }

    private string WriteTypes(List<PokemonTypes> types)
    {
        var descr = new StringBuilder("Type(s): ");
        foreach (var type in types)
            descr.Append($" {type.Name}");
        return descr.ToString();
    }

    private void ClearPokemonInfoPanel()
    {
        pnlAllEvolutions.AutoScroll = false;
        pnlAllEvolutions.VerticalScroll.Visible = false;
        pnlAllEvolutions.AutoScroll = true;
        pnlAllEvolutions.Controls.Clear();
        ClearPanel(pnlImage);
        ClearPanel(pnlPreEvolutionImage);
        txtName.Text = string.Empty;
        txtDescription.Text = string.Empty;
    }

    #endregion Private Methods

    #region Events

    private void btnFirst_Click(object sender, EventArgs e)
    {
        _currentPage = 1;
        ChangePage();
    }

    private void btnPrevious_Click(object sender, EventArgs e)
    {
        _currentPage--;
        ChangePage();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
        _currentPage++;
        ChangePage();
    }

    private void btnLast_Click(object sender, EventArgs e)
    {
        _currentPage = _numberOfPages;
        ChangePage();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        ClearPokemonInfoPanel();
        if (string.IsNullOrWhiteSpace(txtSearchPokemonId.Text))
            return;
        int id = Convert.ToInt32(txtSearchPokemonId.Text);
        _currentPage = (id / _itensPerPage) + 1;
        ChangePage();
        LoadPokemonInfo(id, (int)cbSearchVersion.SelectedValue);
    }

    private void pnlPokemon_Click(object sender, EventArgs e)
    {
        var pnl = (Panel)sender;
        if (pnl?.Tag != null)
        {
            int val = (int)cbVersion.SelectedValue;
            int pokemonId = (int)pnl.Tag;
            ClearPokemonInfoPanel();
            if (pokemonId > 0)
                LoadPokemonInfo(pokemonId, val);
        }
    }

    private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlPokemon_Click(pnlImage, e);
    }

    #endregion Events
}
