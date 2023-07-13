import numpy as np
import pandas as pd
import pyodbc

# Define a function to handle chunks of data
def handle_data(df):
    # Connect to the database
    cnxn = pyodbc.connect('DRIVER={SQL Server};SERVER=192.168.0.104,1433;DATABASE=Csharp_Team;UID=user1;PWD=1234')
    cursor = cnxn.cursor()

    # Use the fast_executemany option
    cursor.fast_executemany = True

    # Define the insert SQL
    insert_sql = "INSERT INTO users (subscription_type, country, gender, average_watch_time, preferred_genre, birth_date, join_date, last_payment_date, device) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)"

    # Execute the insert SQL for all rows
    cursor.executemany(insert_sql, df.values.tolist())

    # Commit the transaction
    cnxn.commit()

# Loop over all CSV files
for i in range(1, 2):
    # Load the data
    df = pd.read_csv(f'data/data ({i}).csv')

    # Handle the data in chunks
    df_chunks = np.array_split(df, 10)  # Change this to adjust the chunk size
    for df_chunk in df_chunks:
        handle_data(df_chunk)
