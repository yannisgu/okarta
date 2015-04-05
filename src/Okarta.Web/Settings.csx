Add("connection", "Data Source=localhost;Initial Catalog=okarta;Integrated Security=True");
if(System.Environment.GetEnvironmentVariable("env") == "prod") {
	Add("insertTestData", false);
	Add("isLocal", true);
}
else {
	Add("insertTestData", true);
	Add("isLocal", true);
}