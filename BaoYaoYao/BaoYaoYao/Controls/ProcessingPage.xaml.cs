using BaoYaoYao.Events;
using CommunityToolkit.Maui.Views;

namespace BaoYaoYao.Controls;

public partial class ProcessingView : Popup
{
	private readonly IEventAggregator eventAggregator;

	public ProcessingView(IEventAggregator eventAggregator)
	{
		InitializeComponent();
		this.eventAggregator = eventAggregator;

		this.eventAggregator.GetEvent<ShowPopupEvent>().Subscribe(x =>
		{
			if (x.NeedClosePopup)
			{
				this.Close();
				return;
			}

			if(string.IsNullOrEmpty(x.UpdateMessage)==false)
			{
				this.Message.Text = x.UpdateMessage;
			}
		});
	}
}