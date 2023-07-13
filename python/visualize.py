import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
import ipywidgets as widgets
from IPython.display import display

# Load the data
df = pd.read_csv('data/data (1).csv')

# Convert date columns to datetime
df['birth_date'] = pd.to_datetime(df['birth_date'])
df['join_date'] = pd.to_datetime(df['join_date'])
df['last_payment_date'] = pd.to_datetime(df['last_payment_date'])

# Calculate age
df['age'] = (pd.to_datetime('2023-06-30') - df['birth_date']).dt.days // 365

# Create a column for last payment year and month
df['last_payment_year_month'] = df['last_payment_date'].dt.to_period('M')

# Function to plot
def plot(option):
    plt.figure(figsize=(10, 6))
    if option == 'Distribution of Users by Country':
        sns.countplot(data=df, x='country')
    elif option == 'Distribution of Users by Gender':
        sns.countplot(data=df, x='gender')
    elif option == 'Distribution of Users by Device':
        sns.countplot(data=df, x='device')
    elif option == 'Distribution of Users by Subscription Type':
        sns.countplot(data=df, x='subscription_type')
    elif option == 'Distribution of Average Watch Time':
        sns.histplot(data=df, x='average_watch_time', bins=30)
    elif option == 'Distribution of Age':
        sns.histplot(data=df, x='age', bins=30)
    elif option == 'Number of Cancellations Over Time':
        cancellations = df[df['last_payment_date'] < pd.to_datetime('2023-06-01')].groupby('last_payment_year_month').size()
        cancellations.plot(kind='line')
        plt.xlabel('Last Payment Year and Month')
        plt.ylabel('Number of Cancellations')
    plt.title(option)
    plt.show()

# Create a dropdown menu
dropdown = widgets.Dropdown(
    options=['Distribution of Users by Country', 'Distribution of Users by Gender', 'Distribution of Users by Device', 'Distribution of Users by Subscription Type', 'Distribution of Average Watch Time', 'Distribution of Age', 'Number of Cancellations Over Time'],
    description='Select plot:',
)

# Use the plot function when the dropdown value changes
widgets.interact(plot, option=dropdown)
