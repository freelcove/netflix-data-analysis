import pandas as pd
import numpy as np
from random import choices
from datetime import datetime

def generate_data(iteration):
    # Load the countries data
    countries = pd.read_csv('data/countries.csv', encoding='utf-8')

    # Load Netflix subscriber data
    subscriber_data = pd.read_csv('data/subscriber_data.csv')

    # Exclude China and Russia
    countries = countries[(countries['name'] != 'China') & (countries['name'] != 'Russia')]

    # Normalize population and GDP per capita to use as weights
    countries['population_weight'] = countries['population'] / countries['population'].sum()
    countries['gdp_per_capita_weight'] = countries['gdp_per_capita'] / countries['gdp_per_capita'].sum()

    # Calculate overall weight for each country as the average of population_weight and gdp_per_capita_weight
    countries['avg_weight'] = (countries['population_weight'] + countries['gdp_per_capita_weight']) / 2

    # Merge countries and subscriber data
    data = pd.merge(countries, subscriber_data, how='left', on='name')

    # Use number of subscribers for the weights where available, otherwise use the average weight
    data['weight'] = np.where(pd.isnull(data['subscribers']), data['avg_weight'], data['subscribers'])

    # Define the number of rows to generate
    num_rows = 1000000  # Change this to the number of rows you want

    # Generate subscription_type with weights
    subscription_type = np.random.choice([1, 2, 3], p=[0.26, 0.53, 0.21], size=num_rows)

    # Generate join_date
    start_date = pd.to_datetime(datetime(2013, 1, 1))
    end_date = pd.to_datetime(datetime(2023, 6, 30))
    date_range = (end_date - start_date).days

    join_days = np.random.randint(0, date_range, num_rows)
    join_date = start_date + pd.to_timedelta(join_days, unit='D')

    # Generate last_payment_date
    # For ongoing subscriptions (98% of users), the last_payment_date is a random date between 2023-06-01 and 2023-06-30
    # For cancelled subscriptions (2% of users), last_payment_date is a random date before 2023-06-01
    last_payment_date = np.where(np.random.rand(num_rows) < 0.02,
                                pd.to_datetime(datetime(2023, 6, 1)) - pd.to_timedelta(np.random.randint(1, date_range, num_rows), unit='D'),
                                pd.to_datetime(datetime(2023, 6, 1)) + pd.to_timedelta(np.random.randint(0, 30, num_rows), unit='D'))

    # Generate country according to population and GDP per capita distribution
    country = choices(data['id'], weights=data['weight'], k=num_rows)

    # Generate gender
    gender = np.random.choice([1, 2], p=[0.49, 0.51], size=num_rows)

    # Generate device
    device = np.random.choice(range(1, 5), p=[0.7, 0.15, 0.1, 0.05], size=num_rows)

    # Define age groups and corresponding probabilities
    age_groups = [13, 18, 25, 35, 50, 65, 90]  # The last group is for ages 65 and above
    probabilities = [0.08, 0.15, 0.30, 0.25, 0.15, 0.07]

    # Generate a categorical series where each category represents an age group
    age_categories = np.random.choice(len(probabilities), p=probabilities, size=num_rows)

    # Initialize an empty array to hold the ages
    ages = np.empty(num_rows)

    # For each age group, generate random ages within the range of that group
    for i in range(len(probabilities)):
        mask = age_categories == i  # Mask for the current age group
        low, high = age_groups[i], age_groups[i+1] if i+1 < len(age_groups) else 80  # Change the last age group to 80
        if high == 80:
            # Use a highly skewed Beta distribution for the last age group
            ages[mask] = np.random.beta(0.5, 5, size=mask.sum()) * (high - low) + low
        else:
            ages[mask] = np.random.beta(2, 5, size=mask.sum()) * (high - low) + low  # Use a Beta distribution


    # Subtract ages from join date to get birth date
    birth_date = (join_date - pd.to_timedelta(ages * 365, unit='D'))

    # Generate preferred_genre
    preferred_genre = np.random.choice(range(1, 11), p=[0.27, 0.15, 0.13, 0.11, 0.09, 0.07, 0.06, 0.05, 0.04, 0.03], size=num_rows)

    # Generate average_watch_time
    average_watch_time = np.random.beta(2, 2, size=num_rows) * 4.8 * 60  # Use a Beta distribution
    average_watch_time = np.clip(average_watch_time, 0, 4.8 * 60).astype(int)  # Clip values to the range [0, 4.8 * 60]

    # Create DataFrame
    df = pd.DataFrame({
        'subscription_type': subscription_type,
        'country': country,
        'gender': gender,
        'average_watch_time': average_watch_time,
        'preferred_genre': preferred_genre,
        'birth_date': birth_date,
        'join_date': join_date,
        'last_payment_date': last_payment_date,
        'device': device,
    })
    
    # Convert datetime to date
    df['birth_date'] = df['birth_date'].dt.date

    # Get the current datetime
    current_datetime = datetime.now().strftime("%Y%m%d%H%M%S")

    # Save to CSV
    df.to_csv(f'data/generated_data_{current_datetime}_{iteration}.csv', encoding='utf-8', index=False)

    print(f"Data saved as generated_data_{current_datetime}_{iteration}.csv")
    
for i in range(1):
    generate_data(i)
