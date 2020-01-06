/* Storage Helper */

var storage = new LocalStorage();

function LocalStorage() {
	this.setData = function (key, value) {
		localStorage.setItem(key, value);
	}

	this.getData = function (key) {
		return localStorage.getItem(key);
	}

	this.containKey = function (key) {
		return localStorage.getItem(key) !== null ? true : false;
	}

	this.clearData = function (key) {
		localStorage.removeItem(key);
	}

	this.clearStorages = function () {
		localStorage.clear();
	}
}

var session = new SessionStorage();

function SessionStorage() {
	this.setData = function (key, value) {
		sessionStorage.setItem(key, value);
	}

	this.getData = function (key) {
		return sessionStorage.getItem(key);
	}

	this.containKey = function (key) {
		return sessionStorage.getItem(key) !== null ? true : false;
	}

	this.clearData = function (key) {
		sessionStorage.removeItem(key);
	}

	this.clearSessions = function () {
		sessionStorage.clear();
	}
}

var cookies = new CookieStorage();

function CookieStorage() {
	this.setData = function (key, value, expireDays = 1) {
		var currentDate = new Date();
		currentDate.setTime(currentDate.getTime() + (expireDays * 24 * 60 * 60 * 1000));
		var expires = 'expires=' + currentDate.toUTCString();
		document.cookie = key + '=' + value + ';' + expires + ';path=/';
	}

	this.getData = function (key) {
		var name = key + '=';
		var decodedCookie = decodeURIComponent(document.cookie);
		var ca = decodedCookie.split(';');
		for (var i = 0; i < ca.length; i++) {
			var c = ca[i];
			while (c.charAt(0) == ' ') {
				c = c.substring(1);
			}
			if (c.indexOf(name) == 0) {
				return c.substring(name.length, c.length);
			}
		}
	}

	this.containKey = function (key) {
		return this.getData(key) ? true : false;
	}

	this.clearData = function (key) {
		this.setData(key, '', -1)
	}

	this.clearCookies = function () {
		document.cookie = '';
	}
}