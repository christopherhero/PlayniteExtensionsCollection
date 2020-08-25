function global:Invoke-LaunchUrl() {
	param (
		$SearchUrlTemplate
	)
	
	# Set gamedatabase
	$Gamedatabase = $PlayniteApi.MainView.SelectedGames

	# Launch urls of all selected gams
	foreach ($game in $Gamedatabase) {
			$SearchUrl = $SearchUrlTemplate -f $($game.name)
			Start-Process $SearchUrl
		}
}
function global:SearchBingImagesWallpaper()
{
	# Set dimensions of image search
	$width = '1920'
	$height = '1080'

	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://www.bing.com/images/search?&q={0} Wallpaper&qft=+filterui:imagesize-custom_' + "$width" + '_' + "$height"
	Invoke-LaunchUrl  $SearchUrlTemplate
}

function global:SearchBingImagesScreenshot()
{
	# Set dimensions of image search
	$width = '1920'
	$height = '1080'

	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://www.bing.com/images/search?&q={0} Screenshot&qft=+filterui:imagesize-custom_ '+ "$width" + '_' + "$height"
	Invoke-LaunchUrl  $SearchUrlTemplate
}
function global:SearchGoogleImagesWallpaper()
{
	# Set dimensions of image search
	$width = '1920'
	$height = '1080'

	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://www.google.com/search?q={0} Wallpaper imagesize:' + "$width" + 'x' + "$height" + '&tbm=isch'
	Invoke-LaunchUrl  $SearchUrlTemplate
}

function global:SearchGoogleImagesScreenshot()
{
	# Set dimensions of image search
	$width = '1920'
	$height = '1080'

	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://www.google.com/search?q={0} Screenshot imagesize:' + "$width" + 'x' + "$height" + '&tbm=isch'
	Invoke-LaunchUrl  $SearchUrlTemplate	
}
function global:SearchMetacritic()
{
	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://www.metacritic.com/search/game/{0}/results' 
	Invoke-LaunchUrl  $SearchUrlTemplate
}

function global:SearchSteamGridDB()
{
	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://www.steamgriddb.com/search/grids?term={0}'
	Invoke-LaunchUrl  $SearchUrlTemplate
}

function global:SearchTwitch()
{
	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://www.twitch.tv/search?term={0}'
	Invoke-LaunchUrl  $SearchUrlTemplate
}
function global:SearchVNDB()
{
	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://vndb.org/v/all?q={0}'
	Invoke-LaunchUrl  $SearchUrlTemplate
}
function global:SearchYoutube()
{
	# Set Search Url template and invoke Url launch function
	$SearchUrlTemplate = 'https://www.youtube.com/results?search_query={0}'
	Invoke-LaunchUrl  $SearchUrlTemplate
}

function global:SearchSteamDB()
{
	# Set gamedatabase
	$GameDatabase = $PlayniteApi.MainView.SelectedGames
	
	# Launch Urls
	foreach ($game in $GameDatabase) {
		if ($game.Source.name -eq "Steam" )
		{
			$SearchUrl = 'https://steamdb.info/app/' + "$($game.GameId)"
			Start-Process  $SearchUrl
		}
		else
		{
			$SearchUrl = 'https://steamdb.info/search/?a=app&q=' + "$($game.name)" + '&type=1&category=0'
			Start-Process  $SearchUrl
		}
	}
}