FROM microsoft/aspnet:1.0.0-beta6

# Set up NGINX for Docker Socket Proxy
RUN echo "deb http://nginx.org/packages/debian/ wheezy nginx" >> /etc/apt/sources.list.d/nginx.list
RUN apt-key adv --fetch-keys "http://nginx.org/keys/nginx_signing.key"

RUN apt-get update && apt-get -y dist-upgrade
RUN apt-get -y install nginx openssl ca-certificates

# forward request and error logs to docker log collector
RUN ln -sf /dev/stdout /var/log/nginx/access.log
RUN ln -sf /dev/stderr /var/log/nginx/error.log
RUN rm -rf /etc/nginx/conf.d/*

RUN chown root:nginx /var/run/docker.sock

ADD nginx.conf /etc/nginx/nginx.conf

CMD ["nginx"]

#Dotker app setup

COPY ./src/Dotker.Compose /app
WORKDIR /app
RUN ["dnu", "restore"]

#ENTRYPOINT ["dnx", ".", "run"]
ENTRYPOINT ["/bin/bash"]