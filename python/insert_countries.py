import pandas as pd
import pyodbc

# Load the data from CSV
df = pd.read_csv('data/countries.csv')

# Create a connection string
conn_str = (
    r'DRIVER={ODBC Driver 17 for SQL Server};'
    r'SERVER=192.168.0.104,1433;'
    r'DATABASE=Csharp_Team;'
    r'UID=user1;'
    r'PWD=1234;'
)

# Create the connection
cnxn = pyodbc.connect(conn_str)
cursor = cnxn.cursor()

# Insert DataFrame to Table
for index, row in df.iterrows():
    cursor.execute("INSERT INTO countries(id,name,region,population,gdp,[gdp_per_capita]) values(?,?,?,?,?,?)", 
                   row['id'], row['name'], row['region'], row['population'], row['gdp'], row['gdp_per_capita'])
cnxn.commit()
cursor.close()
