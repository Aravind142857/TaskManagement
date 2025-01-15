FROM node:18-alpine
COPY package*.json ./
WORKDIR /app
# Install Dependencies
COPY packages*.json ./
RUN npm install
# Copy the rest of the files
COPY . .
# Expose app port
EXPOSE 3000
# Run development mode
CMD ["npm", "run", "dev"]