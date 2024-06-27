using CurrieTechnologies.Razor.SweetAlert2;
using System.Threading.Tasks;

namespace TournamentApp.Services;


public class Alerts
{
	private readonly SweetAlertService _swal;

	public Alerts(SweetAlertService swal)
	{
		_swal = swal;
	}

	public async Task SuccMsg(string msg)
	{
		await _swal.FireAsync(new SweetAlertOptions
		{
			Position = "center",
			Icon = "success",
			Title = msg,
			ShowConfirmButton = false,
			Timer = 1000
		});
	}

	public async Task<bool> ShowConfirmationAlert(string title, string message)
	{
		var result = await _swal.FireAsync(new SweetAlertOptions
		{
            Position = "center",
            Title = title,
			Text = message,
			Icon = "warning",
			ShowCancelButton = true,
			ConfirmButtonColor = "#3085d6",
			CancelButtonColor = "#d33",
			ConfirmButtonText = "Yes, delete it!"
		});

		return !string.IsNullOrEmpty(result.Value);
	}


    public async Task ShowErrorAlert(string title, string message)
    {
        await _swal.FireAsync(new SweetAlertOptions
        {
            Position = "center",
            Icon = "error",
            Title = title,
            Text = message,
            ShowConfirmButton = false,
            Timer = 2000
        });
    }

    public async Task ShowWarningAlert(string title, string message)
    {
        await _swal.FireAsync(new SweetAlertOptions
        {
            Position = "center",
            Icon = "warning",
            Title = title,
            Text = message,
            ShowConfirmButton = false,
            Timer = 2000
        });
    }
}
