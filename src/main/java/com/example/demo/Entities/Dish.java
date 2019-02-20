package com.example.demo.Entities;


import javax.persistence.*;
import java.util.List;

@Entity
public class Dish {

    @Id
    @GeneratedValue
    public Long id;

    public String name;

    @OneToMany(cascade=CascadeType.ALL, fetch = FetchType.LAZY)
    public List<Product> productsofdish;

    @ManyToOne
    public User maker;



    public User getMaker() {
        return maker;
    }

    public void setMaker(User maker) {
        this.maker = maker;
    }

    public Dish(String name, List<Product> productsofdish, User maker) {
        this.name = name;
        this.productsofdish = productsofdish;
        this.maker = maker;
    }

    public Dish() {
    }

    public Dish(String name, List<Product> productsofdish) {
        this.name = name;
        this.productsofdish = productsofdish;
    }

    @Override
    public String toString() {
        return "Dish{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", maker=" + maker +
                '}';
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

    public List<Product> getProductsofdish() {
        return productsofdish;
    }

    public void setProducts(List<Product> products) {
        this.productsofdish = productsofdish;
    }
}
