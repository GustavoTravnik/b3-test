FROM node:16

# Set working directory
WORKDIR /frontend

# Copy package.json and package-lock.json first to install dependencies
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application
COPY . .

# Expose the default Angular development server port
EXPOSE 4200

# Run Angular development server (with the correct host binding)
CMD ["npm", "start"]
