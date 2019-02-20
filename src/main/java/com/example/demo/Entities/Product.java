package com.example.demo.Entities;

import com.example.demo.Entities.User;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToOne;

@Entity
public class Product {

    @Id
    @GeneratedValue
    public Long id;


    public String name;

    public float price;



    public int vat;

    @ManyToOne
    private User creator;


    public User getCreator() {
        return creator;
    }

    public int getVat() {
        return vat;
    }

    public void setVat(int vat) {
        this.vat = vat;
    }
    public Product(String name, float price, User creator, int vat) {
        this.name = name;
        this.price = price;
        this.creator = creator;
        this.vat = vat;
    }

    public void setCreator(User creator) {
        this.creator = creator;
    }

    public Product() {
    }

    public Product(String name, float price) {
        this.name = name;
        this.price = price;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public float getPrice() {
        return price;
    }

    public void setPrice(float price) {
        this.price = price;
    }
}
